using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zebpay.Application.Dal.Contracts;
using Zebpay.Application.Data;
using Zebpay.Application.Entities.Response;
using Zebpay.Web.ViewModels.ViewModels;

namespace Zebpay.Application.Dal.Implementations
{

    public class CurrencyDal : ICurrencyDal
    {

        #region Data Members
        private string ConnectionString = GetConnection.ConnectionString;
        private const string consExchRateSelect = "procExchRateSelect";
        private const string consExchRateCreate = "procExchRateCreate";
        #endregion

        #region Public Methods
        public CurrencyResponse GetCurrencyRate(string currencyCode, decimal amount)
        {
            SqlDataReader theReader = null;

            CurrencyResponse currRead = new CurrencyResponse();

            try
            {
                SqlParameter[] parameters = { new SqlParameter("@pstrCurrCode", SqlDbType.VarChar) };

                parameters[0].Value = currencyCode;

                theReader = SqlHelper.ExecuteReader(ConnectionString, CommandType.StoredProcedure, consExchRateSelect, parameters);

                while (theReader.Read())
                {
                    currRead.SourceCurrency = Convert.ToString(theReader["FromCurrencyCode"].ToString());
                    currRead.ConversionRate = Convert.ToDecimal(theReader["ExchangeRate"].ToString());
                    currRead.CurrRateDate = Convert.ToDateTime(theReader["CurrencyRateDate"]);
                    currRead.Amount = amount;
                    currRead.returncode = 1;
                }

            }
            catch (SqlException exSql)
            {
                //Need to use logger to log the error
                currRead.err = exSql.Message.ToString();
                currRead.returncode = exSql.ErrorCode;
            }
            catch (Exception ex)
            {
                //Need to use logger to log the error
                currRead.err = ex.Message.ToString();
                currRead.returncode = 0;

            }

            finally
            {
                //theReader.Close();
                theReader = null;
            }
            return currRead;
        }
       
        
        /// <summary>
        /// Insert New Rate in DB which pick up from Azure Job Scheduled for every 30 minutes.
        /// </summary>
        /// <returns></returns>
        public async Task<bool> Create(CurrencyViewModel currencyViewModel)
        {
            try
            {
                using (SqlConnection Cn = new SqlConnection(ConnectionString))
                {
                    SqlParameter[] parameters =
                    {
                        new SqlParameter("@pstrCurrCode",SqlDbType.VarChar),
                        new SqlParameter("@pdecExchRate",SqlDbType.Decimal,20),                      
                    };
                    parameters[0].Value = currencyViewModel.SourceCurrency;
                    parameters[1].Value = currencyViewModel.ConversionRate;

                    SqlTransaction Trn;
                    Cn.Open();
                    Trn = Cn.BeginTransaction();

                    try
                    {
                        await SqlHelper.ExecuteNonQueryAsync(Trn, CommandType.StoredProcedure, consExchRateCreate, parameters);                       
                        Trn.Commit();
                    }
                    catch (SqlException exSql)
                    {
                        Trn.Rollback();
                        //Need to use logger to log the error
                    }
                    finally
                    {
                        Trn.Dispose();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return true;
        }
        #endregion

    }
    
}
