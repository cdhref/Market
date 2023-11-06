using Market.Models;
using Market.Models.Context;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Market.Service
{
    public class CompanyService
    {
        private MarketDBContext _context;

        public CompanyService()
        {
            _context = new MarketDBContext();
        }
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

        public void AddCompany(CompanyModel company)
        {
            _context.Company.Add(company);
            _context.SaveChanges();
        }
        public void ModifyCompany(CompanyModel company)
        {
            company.UpdateAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            //_context.Update(company);
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

        public void DeleteCompany(CompanyModel company)
        {
            _context.Company.Attach(company);
            _context.Company.Remove(company);
            _context.SaveChanges();
        }
    }
}
