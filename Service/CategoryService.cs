using Market.Models;
using Market.Models.Context;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Market.Service
{
    public class CategoryService
    {
        private MarketDBContext _context;

        public CategoryService()
        {
            _context = new MarketDBContext();
        }
        public List<CategoryModel> GetCategoryList(string strPage = "")
        {
            int pageNo = 0;
            if (!string.IsNullOrEmpty(strPage))
            {
                pageNo = Int32.Parse(strPage);
            }
            List<CategoryModel> categoryList = _context.Category.ToList();
            return categoryList;
        }

        public void AddCategory(CategoryModel category)
        {
            _context.Add<CategoryModel>(category);
            _context.SaveChanges();
        }
        public void ModifyCategory(CategoryModel category)
        {
            category.UpdateAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            _context.Update(category);
            _context.SaveChanges();
        }

        /// <summary>
        /// 企業情報を1件取得
        /// </summary>
        /// <param name="id">db pk</param>
        /// <returns>企業情報</returns>
        public CategoryModel GetCategory(int id)
        {
            return _context.Find<CategoryModel>(id);
        }

        public void DeleteCategory(CategoryModel category)
        {
            _context.Category.Attach(category);
            _context.Category.Remove(category);
            _context.SaveChanges();
        }
    }
}
