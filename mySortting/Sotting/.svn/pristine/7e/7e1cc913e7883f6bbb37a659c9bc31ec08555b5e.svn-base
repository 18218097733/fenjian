using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SortCommon;
using SortModel;

namespace SortDal
{
    public partial class BarInfoDal
    {
        /// <summary>
        /// 查询获取结果集
        /// </summary>
        /// <returns></returns>
        public List<BarInfo> GetList()
        {
            //构造要查询的sql语句
            string sql = "select * from BarInfo";
            //使用helper进行查询，得到结果
            DataTable dt = SqliteHelper.GetDataTable(sql);
            //将dt中的数据转存到list中
            List<BarInfo> list = new List<BarInfo>();
            foreach(DataRow row in dt.Rows)
            {
                list.Add(new BarInfo()
                {
                    //BId = Convert.ToInt32(row["bid"]),
                    BNumber = row["bnumber"].ToString(),
                   
                    //BType=row["btype"].ToString(),
                    BWeight=Convert.ToDouble(row["bweight"])
                    //Btime=Convert.ToDateTime(row["btime"])
                });
            }
            return list;
        }
        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public int Insert(BarInfo info)
        {
            //构造insert语句
            string sql = "insert into BarInfo(bnumber,btype,bweight,btime) values(@number,@type,@weight,@time)";
            //构造sql语句的参数
            SQLiteParameter[] ps =
            {
                new SQLiteParameter("@number",info.BNumber),
                new SQLiteParameter("@type",info.BType),
                new SQLiteParameter("@weight",info.BWeight),
                new SQLiteParameter("@time",info.Btime)
            };
            return SqliteHelper.ExecuteNonQuery(sql,ps);
        }
        /// <summary>
        /// 根据编号删除数据
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public int Delete(string num)
        {
            //构造删除的sql语句
            string sql = "delete from BarInfo where bnumber=@num";
            //根据语句构造参数
            SQLiteParameter p = new SQLiteParameter("@num", num);
            //执行操作
            return SqliteHelper.ExecuteNonQuery(sql, p);
        }
        /// <summary>
        /// 根据编号查找对象
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public BarInfo GetByNumber(string num)
        {
            //定义一个对象
            BarInfo barInfo = null;
            //构造语句
            string sql = "select * from BarInfo where bnumber=@num";
            //为语句构造参数
            SQLiteParameter p = new SQLiteParameter("@num", num);
            //执行查询结果
            DataTable dt = SqliteHelper.GetDataTable(sql, p);
            //判断是否根据编号找到对象
            if(dt.Rows.Count>0)
            {
                barInfo = new BarInfo()
                {
                    BId = Convert.ToInt32(dt.Rows[0][0]),
                    BNumber = num,
                    BType=dt.Rows[0][2].ToString(),
                    Btime=Convert.ToInt64(dt.Rows[0][3])                   
                };
            }
            else
            {
                //编号不存在
            }
            return barInfo;
        }

        public int Updata(BarInfo barInfo)
        {
            //定义参数集合，可以动态添加元素
            List<SQLiteParameter> listPs = new List<SQLiteParameter>();
            //构造update的sql语句
            string sql = "update BarInfo set btime=@time where bnumber=@number";
            listPs.Add(new SQLiteParameter("@time", barInfo.Btime));
            listPs.Add(new SQLiteParameter("@number", barInfo.BNumber));
            //执行语句并返回结果
            return SqliteHelper.ExecuteNonQuery(sql, listPs.ToArray());
        }
        

    }

}
