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
        public List<ProductModel> GetProductList(string strPage)
        {
            int pageNo = 0;
            if (!string.IsNullOrEmpty(strPage))
            {
                pageNo = Int32.Parse(strPage);
            }
            List<ProductModel> productList = _context.Product.ToList();
            return productList;
        }

        public void AddProduct(ProductModel product)
        {
            _context.Add<ProductModel>(product);
            _context.SaveChanges();
        }
        public void ModifyProduct(ProductModel product)
        {
            product.UpdateAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            _context.Update(product);
            _context.SaveChanges();
        }

        /// <summary>
        /// 商品情報を1件取得
        /// </summary>
        /// <param name="id">db pk</param>
        /// <returns>商品情報</returns>
        public ProductModel GetProduct(int id)
        {
            return _context.Find<ProductModel>(id);
        }

        public void DeleteProduct(ProductModel product)
        {
            _context.Product.Attach(product);
            _context.Product.Remove(product);
            _context.SaveChanges();
        }
    }
}
