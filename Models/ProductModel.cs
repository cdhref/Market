using Market.Object.Interface;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Market.Models
{
    [Table("Product")]
    public class ProductModel : IDBModel
    {
        public int ID { get; set; }                  // db pk
        public int CompanyID { get; set; }           // 企業id fk
        public int CategoryID { get; set; }          // カテゴリid fk
        public string Name { get; set; }             // 商品名
        public int Price { get; set; }               // 価格
        public string Comment { get; set; }          // 商品説明
        public string CreateAt { get; set; }         // データ登録日
        public string UpdateAt { get; set; }         // データ修正日

        public virtual CompanyModel Company { get; set; }
        public virtual CategoryModel Category { get; set; }

        public ProductModel() { }

        /// <summary>
        /// 必需データが入力されたか確認。
        /// </summary>
        /// <returns>true: 入力済み</returns>
        public bool CheckNotNullDataAndSetDefault() { 
            if(string.IsNullOrEmpty(Name))
            {
                return false;
            }
            if (CategoryID == 0)
            {
                return false;
            }
            if (CompanyID == 0)
            {
                return false;
            }
            if (Price == 0)
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
