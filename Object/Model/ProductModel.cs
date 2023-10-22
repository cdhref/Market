using Market.Object.Interface;
using System;

namespace Market.Models
{
    public class ProductModel : IDBModel
    {
        public int Id { get; set; }                  // db pk
        public CompanyModel Company { get; set; }    // 企業情報
        public CategoryModel Category { get; set; }  // カテゴリ情報
        public string Name { get; set; }             // 商品名
        public int Price { get; set; }               // 価格
        public string Comment { get; set; }          // 商品説明
        public string CreateAt { get; set; }         // データ登録日
        public string UpdateAt { get; set; }         // データ修正日

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
            if (Category == null || Category.Id == 0)
            {
                return false;
            }
            if (Company == null || Company.Id == 0)
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
