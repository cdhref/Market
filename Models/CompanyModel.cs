using Market.Object.Interface;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Market.Models
{
    [Table("Company")]
    public class CompanyModel : IDBModel
    {
        public int ID { get; set; }                  // db pk
        public string Name { get; set; }             // 企業名
        public string Comment { get; set; }          // コメント
        public string IncorporationAt { get; set; }  // 設立日
        public string CreateAt { get; set; }         // データ登録日
        public string UpdateAt { get; set; }         // データ修正日

        public CompanyModel() { }
        public CompanyModel(string name, string comment, string incorporationAt, string createAt, string updateAt)
        {
            Name = name;
            Comment = comment;
            IncorporationAt = incorporationAt;
            CreateAt = createAt;
            UpdateAt = updateAt;
        }

        /// <summary>
        /// 必需データが入力されたか確認。
        /// </summary>
        /// <returns>true: 入力済み</returns>
        public bool CheckNotNullDataAndSetDefault() { 
            if(string.IsNullOrEmpty(Name))
            {
                return false;
            }
            if (string.IsNullOrEmpty(IncorporationAt))
            {
                return false;
            }

            DateTime now = DateTime.Now;
            // default data set
            if (string.IsNullOrEmpty(CreateAt))
            {
                CreateAt = now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            }
            if (string.IsNullOrEmpty(UpdateAt))
            {
                UpdateAt = now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            }
            return true;
        }
    }
}
