using Market.Models;
using Market.Models.Context;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Market.Service
{
    public class CategoryService
    {
        private readonly MarketDBContext _context;

        public CategoryService()
        {
            _context = new MarketDBContext();
        }

        /// <summary>
        /// Category List取得
        /// </summary>
        /// <param name="strPage"></param>
        /// <returns>Category List</returns>
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

        /// <summary>
        /// Category情報を1件登録
        /// </summary>
        /// <param name="param">登録対象のデータ</param>
        public void AddCategory(CategoryModel category)
        {
            _context.Category.Add(category);
            _context.SaveChanges();
        }

        /// <summary>
        /// Category情報を1件修正
        /// </summary>
        /// <param name="param">修正対象のデータ</param>
        public void ModifyCategory(CategoryModel param)
        {
            var category = _context.Category.FirstOrDefault<CategoryModel>(c => c.ID == param.ID);
            category.UpdateAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            category.Name = param.Name;
            _context.SaveChanges();
        }

        /// <summary>
        /// Category情報を1件取得
        /// </summary>
        /// <param name="id">db pk</param>
        /// <returns>Category情報</returns>
        public CategoryModel GetCategory(int id)
        {
            return _context.Category.Find(id);
        }

        /// <summary>
        /// Category情報を1件削除
        /// </summary>
        /// <param name="category">削除対象のデータ</param>
        public void DeleteCategory(CategoryModel category)
        {
            _context.Category.Attach(category);
            _context.Category.Remove(category);
            _context.SaveChanges();
        }
    }
}
