using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WetterInfo.DataAccess.Repository.IRepository;
using WetterInfo.Models;

namespace WetterInfo.DataAccess.Repository
{
    public class ResponseRepository : IResponseRepository
    {
        private readonly ApplicationDbContext _db;
        internal DbSet<ResultView> dbSet;

        public ResponseRepository(ApplicationDbContext db)
        {
            _db = db;
            this.dbSet = _db.Set<ResultView>();
        }

        public void Add(ResultView responseObj)
        {
            dbSet.Add(responseObj);
            _db.SaveChanges();
        }
    }
}
