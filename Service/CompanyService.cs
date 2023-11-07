using Market.Models;
using Market.Models.Context;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Market.Service
{
    public class CompanyService
    {
        private readonly MarketDBContext _context;

        public CompanyService()
        {
            _context = new MarketDBContext();
        }

        /// <summary>
        /// 企業 List取得
        /// </summary>
        /// <param name="strPage"></param>
        /// <returns>企業 List</returns>
        public List<CompanyModel> GetCompanyList(string strPage = "")
        {
            int pageNo = 0;
            if (!string.IsNullOrEmpty(strPage))
            {
                pageNo = Int32.Parse(strPage);
            }
            List<CompanyModel> companyList = _context.Company.ToList();
            return companyList;
        }

        /// <summary>
        /// 企業情報を1件追加
        /// </summary>
        /// <param name="company">追加対象のデータ</param>
        public void AddCompany(CompanyModel company)
        {
            _context.Company.Add(company);
            _context.SaveChanges();
        }

        /// <summary>
        /// 企業情報を1件修正
        /// </summary>
        /// <param name="param">修正対象のデータ</param>
        public void ModifyCompany(CompanyModel param)
        {
            var company = _context.Company.FirstOrDefault<CompanyModel>(c => c.ID == param.ID);
            company.UpdateAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            company.Name = param.Name;
            company.Comment = param.Comment;
            company.IncorporationAt = param.IncorporationAt;
            _context.SaveChanges();
        }

        /// <summary>
        /// 企業情報を1件取得
        /// </summary>
        /// <param name="id">db pk</param>
        /// <returns>企業情報</returns>
        public CompanyModel GetCompany(int id)
        {
            return _context.Company.Find(id);
        }

        /// <summary>
        /// 企業情報を1件削除
        /// </summary>
        /// <param name="company">削除対象のデータ</param>
        public void DeleteCompany(CompanyModel company)
        {
            _context.Company.Attach(company);
            _context.Company.Remove(company);
            _context.SaveChanges();
        }
    }
}
