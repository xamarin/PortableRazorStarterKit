using System;

namespace PortableCongress
{
	public class Vote
	{
		public string Question { get; set; }
		public string Value { get; set; }
		public string Link { get; set; }
        public string RelatedBillId { get ; set; }
		public DateTime PublicationDate { get; set; }
	}
}