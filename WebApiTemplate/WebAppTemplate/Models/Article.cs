using System;
using System.Data;

namespace WebAppTemplate.Models
{
    public partial class Article
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public Nullable<decimal> Quantity { get; set; }

        public Article() { }

        public Article(DataRow dr)
        {
            Id = Convert.ToInt64(dr["Id"]);
            Code = Convert.ToString(dr["Code"]);
            Description = Convert.ToString(dr["Description"]);
            Quantity = (dr["Quantity"] == System.DBNull.Value) ? (decimal?)null : Convert.ToDecimal(dr["Quantity"]);
        }
    }
}