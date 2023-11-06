using Market.Object.Interface;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Market.Models
{
    [Table("Category")]
    public class CategoryModel : IDBModel
    {
        public int ID { get; set; }           // db pk
        public string Name { get; set; }      // 企業名
        public string CreateAt { get; set; }  // データ登録日
        public string UpdateAt { get; set; }  // データ修正日

        public CategoryModel() { }

        /// <summary>
        /// 必需データが入力されたか確認。
        /// </summary>
        /// <returns>true: 入力済み</returns>
        public bool CheckNotNullDataAndSetDefault() { 
            if(string.IsNullOrEmpty(Name))
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
