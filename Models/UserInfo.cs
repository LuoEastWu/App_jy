using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    [SugarTable("UserInfo")]
    public class UserInfo
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true, ColumnName = "Id")]
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Pwd { get; set; }
    }
}
