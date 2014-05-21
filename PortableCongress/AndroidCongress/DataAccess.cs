using System;
using System.Collections.Generic;
using Mono.Data.Sqlite;
using PortableCongress;

namespace Congress
{
	public partial class DataAccess : IDataAccess
	{
		string connectionString;

		public Politician LoadPolitician (int id)
		{
			var politician = new Politician ();

			using (var connection = new SqliteConnection (connectionString)) {
				using (var cmd = connection.CreateCommand ()) {
					connection.Open ();

					cmd.CommandText = String.Format ("SELECT bioguide_id, first_name, last_name, govtrack_id, phone, state, party, address FROM Politician WHERE govtrack_id = '{0}'", id);

					using (var reader = cmd.ExecuteReader ()) {
						if (reader.Read ()) {
							politician.FirstName = reader ["first_name"].ToString ();
							politician.LastName = reader ["last_name"].ToString ();
							politician.BioGuideId = reader ["bioguide_id"].ToString ();
							politician.GovTrackId = reader ["govtrack_id"].ToString ();
							politician.Phone = reader ["phone"].ToString ();
							politician.State = reader ["state"].ToString ();
							politician.Party = reader ["party"].ToString ();
							politician.OfficeAddress = reader ["address"].ToString ();
						}
					}
				}
			}
			return politician;
		}

		public List<Politician> LoadAllPoliticans ()
		{
			var politicians = new List<Politician> ();

			using (var connection = new SqliteConnection (connectionString)) {
				using (var cmd = connection.CreateCommand ()) {
					connection.Open ();
                    cmd.CommandText = String.Format ("SELECT bioguide_id, first_name, last_name,  govtrack_id, phone, party, state FROM Politician ORDER BY last_name");

					using (var reader = cmd.ExecuteReader ()) {
						while (reader.Read ()) {
							politicians.Add (new Politician { 
								FirstName = reader ["first_name"].ToString (), 
								LastName = reader ["last_name"].ToString (),
								BioGuideId = reader ["bioguide_id"].ToString (),  
								GovTrackId = reader ["govtrack_id"].ToString (), 
                                Phone = reader ["phone"].ToString (),
                                State = reader ["state"].ToString (),
                                Party = reader ["party"].ToString ()
							});
						}
					}
				}
			}
			return politicians;
		}

        public void SaveFavoriteBill (Bill bill)
        {
            using (var connection = new SqliteConnection (connectionString)) {
                using (var cmd = connection.CreateCommand ()) {
                    connection.Open ();

                    string sql = "INSERT OR REPLACE Into FavoriteBills (id, title, thomas_link) Values (@id, @title, @thomas_link)";

                    var idParam = new SqliteParameter ("@id", bill.Id);
                    var titleParam = new SqliteParameter ("@title", bill.Title);
                    var thomas_linkParam = new SqliteParameter ("@thomas_link", bill.ThomasLink);

                    cmd.Parameters.Add (idParam); 
                    cmd.Parameters.Add (titleParam); 
                    cmd.Parameters.Add (thomas_linkParam); 
                    cmd.CommandText = sql;
                    cmd.ExecuteNonQuery ();
                }
            }
        }

        public void DeleteFavoriteBill (int id){
            using (var connection = new SqliteConnection (connectionString)) {
                using (var cmd = connection.CreateCommand ()) {
                    connection.Open ();

                    string sql = "DELETE FROM FavoriteBills Where id=@id";
                    var idParam = new SqliteParameter ("@id", id);
                    cmd.Parameters.Add (idParam);
                    cmd.CommandText = sql;
                    cmd.ExecuteNonQuery ();
                }
            }
        }

        public List<Bill> LoadFavoriteBills () {
            var bills = new List<Bill> ();

            using (var connection = new SqliteConnection (connectionString)) {
                using (var cmd = connection.CreateCommand ()) {
                    connection.Open ();

                    cmd.CommandText = "SELECT * FROM FavoriteBills";

                    using (var reader = cmd.ExecuteReader ()) {
                        while (reader.Read ()) {
                            bills.Add (new Bill { 
                                Id = Convert.ToInt32(reader ["id"]),
                                Title = (string)reader ["title"],
                                ThomasLink = (string)reader ["thomas_link"]
                            });
                        }
                    }
                }
            }

            return bills;
        }

        public Bill LoadFavoriteBill (int id)
        {
            Bill favBill;

            using (var connection = new SqliteConnection (connectionString)) {
                using (var cmd = connection.CreateCommand ()) {
                    connection.Open ();

                    cmd.CommandText = "SELECT * FROM FavoriteBills WHERE id = @id";
                    var idParam = new SqliteParameter ("@id", id);
                    cmd.Parameters.Add (idParam);

                    using (var reader = cmd.ExecuteReader ()) {
                        reader.Read ();
                        favBill = new Bill { 
                            Id = Convert.ToInt32 (reader ["id"]),
                            Title = (string)reader ["title"],
                            ThomasLink = (string)reader ["thomas_link"],
                            Notes = reader["notes"] == DBNull.Value ? "" : (string)reader["notes"]
                        };
                    }
                }
            }

            return favBill;
        }

        public void SaveNotes (int id, string notes)
        {
            using (var connection = new SqliteConnection (connectionString)) {
                using (var cmd = connection.CreateCommand ()) {
                    connection.Open ();

                    string sql = "UPDATE FavoriteBills SET notes = @notes WHERE id = @id";

                    var idParam = new SqliteParameter ("@id", id);
                    var notesParam = new SqliteParameter ("@notes", notes);

                    cmd.Parameters.Add (idParam); 
                    cmd.Parameters.Add (notesParam); 

                    cmd.CommandText = sql;
                    cmd.ExecuteNonQuery ();
                }
            }
        }
	}
}