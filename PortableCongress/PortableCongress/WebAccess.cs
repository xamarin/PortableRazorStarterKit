using System;
using System.Threading.Tasks;
using System.Net.Http;
using System.IO;
using System.Xml.Linq;
using System.Linq;
using System.Collections.Generic;

namespace PortableCongress
{
    public class WebAccess
    {
        const string API_KEY = "609b2b4b92f74ce9ac5c86487146d107";

        public static async Task<RecentVotes> GetRecentVotesAsync (int id)
        {
            try {
                using (var httpClient = new HttpClient ()) {
                 
                    string url= String.Format("https://www.govtrack.us/api/v2/vote_voter?person={0}&limit=50&sort=-created&format=xml", id);

                    var response = await httpClient.GetAsync (url);
                    var stream = await response.Content.ReadAsStreamAsync ();
                    var votes = LoadVotes (stream);
                    var recentVotes = new RecentVotes { Id = id, Votes = votes };

                    return recentVotes;
                }
            } catch (Exception) {
                var recentVotes = new RecentVotes {Id = id, Votes = new List<Vote> {
                        new Vote { Question = "Could not connect to the internet" }
                    }
                };
                return recentVotes;
            }
        }

        public static async Task<Committees> GetCommitteesAsync (int id, string bioGuideId)
        {
            try {
                using (var httpClient = new HttpClient ()) {
                    string url = String.Format ("http://services.sunlightlabs.com/api/committees.allForLegislator.xml?apikey={0}&bioguide_id={1}", API_KEY, bioGuideId);

                    var response = await httpClient.GetAsync (url);
                    var stream = await response.Content.ReadAsStreamAsync ();
                    var committeeList = LoadCommittees (stream);
                    var committees = new Committees { Id = id, CommitteeList = committeeList };

                    return committees;	
                }
            } catch (Exception) {
                var commitees = new Committees {Id = id, CommitteeList = new List<Committee> {
                        new Committee { Name = "Could not connect to the internet" }
                    }
                };
                return commitees;
            }
        }

        public static async Task<Bill> GetBillAsync (int id)
        {
            try {
                using (var httpClient = new HttpClient ()) {

                    string url = String.Format ("https://www.govtrack.us/api/v2/bill/{0}?format=xml", id);

                    var response = await httpClient.GetAsync (url);
                    var stream = await response.Content.ReadAsStreamAsync ();
                    var bill = LoadBill(stream, id);

                    return bill;
                }
            } catch (Exception) {
                var bill = new Bill { Id = -1, Title = "Could not connect to the internet" };
                return bill;
            }
        }

        static List<Committee> LoadCommittees (Stream stream)
        {
            XDocument committeeData = XDocument.Load (stream);

            var committeeList = (from c in committeeData.Descendants ("committee")
                                 select new Committee { Name = c.Element ("name").Value }).ToList ();

            return committeeList;
        }

        static List<Vote> LoadVotes (Stream stream)
        {
            XDocument voteFeed = XDocument.Load (stream);

            var votes = (from item in voteFeed.Descendants ("item")
                select new Vote {
                    Question = item.Element ("vote").Element("question").Value,
                    PublicationDate = DateTime.Parse (item.Element ("vote").Element ("created").Value),
                    Link = item.Element ("vote").Element ("link").Value,
                    Value = item.Element("option").Element("value").Value,
                    RelatedBillId = item.Element ("vote").Element("related_bill").Value
                }).OrderByDescending (v => v.PublicationDate).ToList ();

            return votes;
        }

        static Bill LoadBill (Stream stream, int id)
        {
            XDocument billXml = XDocument.Load (stream);

            var bill = new Bill {
                Title = (string)(from title in billXml.Descendants ("title_without_number")
                    select title).First (),
                ThomasLink = (string)(from link in billXml.Descendants ("thomas_link")
                    select link).First (),
                Id = id
            };

            return bill;
        }
    }
}

