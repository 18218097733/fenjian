using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SortDal;
using SortModel;
using SortCommon;

namespace SortBall
{
    public partial class BarInfoBll
    {
        //创建数据层对象
        BarInfoDal infoDal = new BarInfoDal();

        public List<BarInfo> GetList()
        {
            try
            {
                //获取全部数据
                return infoDal.GetList();
            }
            catch(Exception ex)
            {
                List<BarInfo> list = new List<BarInfo>();
                list.Add(new BarInfo()
                {
                    BNumber = "读取数据库失败！！",
      
                });
                return list;
            }
        }
        
        public bool Add(BarInfo barInfo)
        {
            return infoDal.Insert(barInfo) > 0;
        }

        public bool Edit(BarInfo barInfo)
        {
            return infoDal.Updata(barInfo) > 0;
        }
        public void UpSqlite(BarInfo barInfo)
        {
            //根据编号查询结果
            BarInfo barInfo2 = infoDal.GetByNumber(barInfo.BNumber);
            if(barInfo2==null)
            {
                infoDal.Insert(barInfo);
            }
            else
            {
                long nowtick = TimeTransitionHelper.GetCurrentTimeUnix();
                if(nowtick-barInfo.Btime>1200)//20分钟后可再次扫码
                {
                    infoDal.Updata(barInfo);
                }
            }
        }
    }
}
