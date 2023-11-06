using Market.Models;
using Market.Models.Context;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Market.Service
{
    public class ProductService
    {
        private readonly MarketDBContext _context;

        public ProductService()
        {
            _context = new MarketDBContext();
        }
        private ProductModel MapToProductModel(ProductModel product, CategoryModel category, CompanyModel company)
        {
            return new ProductModel
            {
                ID = product.ID,
                Name = product.Name,
                Price = product.Price,
                Comment = product.Comment,
                CreateAt = product.CreateAt,
                UpdateAt = product.UpdateAt,
                Category = category,
                Company = company,
            };
        }

        public List<ProductModel> GetProductList(string strPage)
        {
            int pageNo = 0;
            if (!string.IsNullOrEmpty(strPage))
            {
                pageNo = Int32.Parse(strPage);
            }
            var query = from product in _context.Product
                        join category in _context.Category on product.CategoryID equals category.ID
                        join company in _context.Company on product.CompanyID equals company.ID
                        select new
                        {
                            product,
                            category,
                            company
                        };

            List<ProductModel> productList = query.ToList().Select(item => MapToProductModel(item.product, item.category, item.company)).ToList<ProductModel>();

            return productList;
        }

        public void AddProduct(ProductModel product)
        {
            _context.Product.Add(product);
            _context.SaveChanges();
        }
        public void ModifyProduct(ProductModel param)
        {
            var company = _context.Product.FirstOrDefault<ProductModel>(c => c.ID == param.ID);
            company.UpdateAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            company.Name = param.Name;
            company.Comment = param.Comment;
            company.CompanyID = param.CompanyID;
            company.CategoryID = param.CategoryID;
            company.Price = param.Price;
            _context.SaveChanges();
        }

        /// <summary>
        /// 商品情報を1件取得
        /// </summary>
        /// <param name="id">db pk</param>
        /// <returns>商品情報</returns>
        public ProductModel GetProduct(int id)
        {
            return _context.Product.FirstOrDefault(p => p.ID == id);
        }

        /// <summary>
        /// 商品データを削除する
        /// </summary>
        /// <param name="product">商品</param>
        public void DeleteProduct(ProductModel product)
        {
            _context.Product.Attach(product);
            _context.Product.Remove(product);
            _context.SaveChanges();
        }

        /// <summary>
        /// 指定された企業の月別登録件数を取得する。
        /// </summary>
        /// <param name="companyID"></param>
        /// <returns></returns>
        internal List<ProductAddedCount> GetProductAddedCount(int companyID)
        {
            DateTime oneYearAgo = DateTime.Now.AddYears(-1);

            var allProducts = _context.Product.ToList(); // 데이터를 로컬로 가져옴

            List<ProductAddedCount> monthlyRegistrations = allProducts
                .Where(p => p.CompanyID == companyID && DateTime.ParseExact(p.CreateAt, "yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture) >= oneYearAgo)
                .Select(p =>
                {
                    var createAt = DateTime.ParseExact(p.CreateAt, "yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture);
                    return new
                    {
                        p.CompanyID,
                        Year = createAt.Year,
                        Month = createAt.Month
                    };
                })
                .GroupBy(p => new
                {
                    p.CompanyID,
                    p.Year,
                    p.Month
                })
                .Select(g => new ProductAddedCount
                {
                    YYYYMM = string.Format("{0}/{1:D2}", g.Key.Year, g.Key.Month),
                    Count = g.Count()
                })
                .OrderBy(g => g.YYYYMM)
                .ToList();

            return monthlyRegistrations;
        }
    }
}
