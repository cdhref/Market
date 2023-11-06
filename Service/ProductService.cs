using Market.Models;
using Market.Models.Context;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Market.Service
{
    public class ProductService
    {
        private MarketDBContext _context;

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
            //_context.Add<ProductModel>(product);
            _context.SaveChanges();
        }
        public void ModifyProduct(ProductModel product)
        {
            product.UpdateAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            //_context.Update(product);
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
    }
}
