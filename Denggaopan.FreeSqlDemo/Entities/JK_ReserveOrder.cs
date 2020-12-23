using FreeSql.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Denggaopan.FreeSqlDemo.Entities
{

    [Table(Name = "JK_ReserveOrder")]
    public class ReserveOrder
    {
        ///<summary>
        ///
        ///</summary>
        [Column(IsIdentity = true, IsPrimary = true)]
        public int ReserveOrderID { get; set; }

        ///<summary>
        ///商家ID
        ///</summary>
        public int BusinessID { get; set; }

        ///<summary>
        ///门店ID
        ///</summary>
        public int StoreID { get; set; }

        ///<summary>
        ///
        ///</summary>
        public string StoreName { get; set; }

        ///<summary>
        ///门店电话
        ///</summary>
        public string StorePhone { get; set; }

        ///<summary>
        ///会员ID
        ///</summary>
        public int CustomerID { get; set; }

        ///<summary>
        ///会员微信OpenID
        ///</summary>
        public string CustomerOpenId { get; set; }

        ///<summary>
        ///会员姓名
        ///</summary>
        public string CustomerName { get; set; }

        /// <summary>
        /// 会员头像
        /// </summary>
        public string CustomerAvatar { get; set; }

        ///<summary>
        ///会员手机号
        ///</summary>
        public string CustomerPhone { get; set; }

        ///<summary>
        ///预订日期
        ///</summary>
        public DateTime ScheduledDate { get; set; }

        ///<summary>
        ///预计到店时间
        ///</summary>
        public string ArrivalTime { get; set; }

        ///<summary>
        ///预订人数
        ///</summary>
        public int PeopleNumber { get; set; }

        ///<summary>
        ///预订餐别ID
        ///</summary>
        public int MealTypeID { get; set; }

        ///<summary>
        ///预订餐别名称
        ///</summary>
        public string MealTypeName { get; set; }

        ///<summary>
        ///房型ID
        ///</summary>
        public int RoomID { get; set; }

        ///<summary>
        ///房型名称
        ///</summary>
        public string RoomName { get; set; }

        ///<summary>
        ///预订房间数
        ///</summary>
        public int OrderRoomCount { get; set; }

        ///<summary>
        ///应付订金金额
        ///</summary>
        public decimal OrderPayableMoney { get; set; }

        ///<summary>
        ///已付订金总金额
        ///</summary>
        public decimal OrderPayedMoney { get; set; }

        ///<summary>
        ///已转储值总金额
        ///</summary>
        public decimal StorageStateMoney { get; set; }

        ///<summary>
        ///订单状态
        ///</summary>
        public string OrderStatus { get; set; }

        ///<summary>
        ///订单状态文字信息
        ///</summary>
        public string OrderStatusText { get; set; }

        ///<summary>
        ///订单状态最后更新时间
        ///</summary>
        public DateTime LastUpdateStatusTime { get; set; }

        ///<summary>
        ///订单备注
        ///</summary>
        public string Remark { get; set; }

        ///<summary>
        ///预订菜品总数
        ///</summary>
        public decimal ReserveFoodCount { get; set; }

        ///<summary>
        ///预订创建时间
        ///</summary>
        public DateTime CreateTime { get; set; }

        ///<summary>
        ///订单来源
        ///</summary>
        public string OrderFrom { get; set; }

        ///<summary>
        ///已分配房间
        ///</summary>
        public int HasAssignedRoom { get; set; }

        ///<summary>
        ///分配的房间（逗号分隔）
        ///</summary>
        public string AssignedRoomText { get; set; }

        ///<summary>
        ///客服人员
        ///</summary>
        public int ServiceEmployeeID { get; set; }

        ///<summary>
        ///客服人员openid
        ///</summary>
        public string ServiceOpenID { get; set; }

        ///<summary>
        ///客服人员姓名
        ///</summary>
        public string ServiceName { get; set; }

        ///<summary>
        ///客服人员电话
        ///</summary>
        public string ServicePhone { get; set; }

        ///<summary>
        ///客服人员订单备注
        ///</summary>
        public string ServiceOrderRemark { get; set; }

        ///<summary>
        ///订单取消备注
        ///</summary>
        public string OrderCancelRemark { get; set; }

        ///<summary>
        ///订单线下已同步状态
        ///</summary>
        public string OffLineSyncStatus { get; set; }

        ///<summary>
        ///转接单状态
        ///</summary>
        public int ZhuanjieStatus { get; set; }

        ///<summary>
        ///有效订单汇总
        ///</summary>
        public int ValidOrderCollect { get; set; }

        public int? TsOrderId { get; set; }

    }
}
