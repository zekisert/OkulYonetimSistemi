using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using AbcYazilim.Dal.Interfaces;
using AbcYazilim.OgrenciTakip.Common.Message;

namespace AbcYazilim.Dal.Base
{
    public class UnitOfWork<T> : IUnitOfWork<T> where T : class
    {
        #region Variables
        private readonly DbContext _context; 
        #endregion

        public UnitOfWork(DbContext context)
        {
            if (context == null) return;
            _context = context;
        }

        private bool disposedValue;

        public IRepository<T> Rep => new Repository<T>(_context);

        public bool Save()
        {
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                var sqlEx = (SqlException) ex.InnerException?.InnerException;
                if (sqlEx == null)
                {
                    Messages.HataMesaji(ex.Message);
                    return false;
                }

                switch (sqlEx.Number)
                {
                    case 208:
                        Messages.HataMesaji("İşlem Yapmak İstediğiniz Tablo Veritabanında Bulunamadı.");
                        break;
                    case 547:
                        Messages.HataMesaji("Seçilen Kartın İşlem Görmüş Hareketleri Var Kart Silinemez.");
                        break;
                    case 2601:
                    case 2627:
                        Messages.HataMesaji("Girmiş Olduğunuz Id Daha Önce Kullanılmıştır.");
                        break;
                    case 4060:
                        Messages.HataMesaji("İşlem Yapmak İstediğiniz Veritabanı Sunucuda Bulunamadı.");
                        break;
                    case 18456:
                        Messages.HataMesaji("Server'a Bağlanılmak İstenilen Kullanıcı Adı ve Şifre Hatalıdır.");
                        break;
                    default:
                        Messages.HataMesaji(sqlEx.Message);
                        break;
                }

                return false;
            }
            catch (Exception ex)
            {
                Messages.HataMesaji(ex.Message);
                return false;
            }

            return true;
        }

        #region Dispose
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _context.Dispose();
                }


                disposedValue = true;
            }
        }


        public void Dispose()
        {

            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        } 
        #endregion
    }
}