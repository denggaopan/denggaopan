using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Denggaopan.DapperDemo.Entities
{
    public partial class JK_ThreeclssBusinessConfig
    {

        /// <summary>
        /// 商家guid
        /// </summary>
        public string BusinessGuid { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateTime { get; set; }

        /// <summary>
        /// 更新人id
        /// </summary>
        public string UpdatorId { get; set; }

        /// <summary>
        /// 更新人名字
        /// </summary>
        public string UpdatorName { get; set; }

        public int MyProperty { get; set; }
    }
}
