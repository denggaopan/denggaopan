using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Denggaopan.UidGenerator
{
    public class UidGen
    {
        //基准时间 （10毫秒）
        public const long BaseTimestamp = 160943040000L; //2021-01-01 00:00:00
        //机器ID位数
        const int WorkerIdBits = 16;

        //序列号位数
        const int SequenceBits = 10;

        //机器ID最大值
        const long MaxWorkerId =  (1L << WorkerIdBits)-1;

        //序列号ID最大值
        private const long SequenceMask =(1L << SequenceBits)-1;

        //机器ID偏左移位
        private const int WorkerIdShift = SequenceBits;

        //时间毫秒左移位
        public const int TimestampLeftShift = SequenceBits + WorkerIdBits;

        private long _sequence = 0L;
        private long _lastTimestamp = -1L;

        public string Ip { get; protected set; }
        public long WorkerId { get; protected set; }
        public long DataCenterId { get; protected set; }
        public long Sequence
        {
            get { return _sequence; }
            internal set { _sequence = value; }
        }

        public UidGen(string ip, long workerId, long sequence = 0L)
        {
            // 如果超出范围就抛出异常
            if (workerId > MaxWorkerId || workerId < 0)
            {
                throw new ArgumentException($"ip={Ip}, workerId={workerId} 必须大于0，且不能大于MaxWorkerId： {MaxWorkerId}");
            }

            //先检验再赋值
            WorkerId = workerId;
            _sequence = sequence;
        }

        readonly object _lock = new Object();
        public virtual long NextId()
        {
            lock (_lock)
            {
                var timestamp = TimestampGen();
                if (timestamp < _lastTimestamp)
                {
                    var overtime = _lastTimestamp - timestamp;
                    //Thread.Sleep();
                }
                else if (_lastTimestamp == timestamp)
                {
                    //如果上次生成时间和当前时间相同,在同一时刻内
                    //sequence自增，和sequenceMask相与一下，去掉高位
                    //判断是否溢出,也就是每毫秒内超过序号最大值，当为序号最大值时，与sequenceMask相与，sequence就等于0
                    _sequence++;
                    if(_sequence > SequenceMask)
                    {
                        _sequence = 0;
                    }
                    if (_sequence == 0)
                    {
                        //等待到下一毫秒
                        timestamp = NextTimestamp(_lastTimestamp);
                    }
                }
                else
                {
                    //如果和上次生成时间不同,重置sequence，就是下一时刻开始，sequence计数重新从0开始累加,
                    //为了保证尾数随机性更大一些,第一位可以设置一个随机数
                    _sequence = new Random().Next(10); //0;
                }

                _lastTimestamp = timestamp;
                return ((timestamp - BaseTimestamp) << TimestampLeftShift) | (WorkerId << WorkerIdShift) | _sequence;
            }
        }

        // 防止产生的时间比之前的时间还要小（由于NTP回拨等问题）,保持增量的趋势.
        protected virtual long NextTimestamp(long lastTimestamp)
        {
            var timestamp = TimestampGen();
            while (timestamp <= lastTimestamp)
            {
                timestamp = TimestampGen();
            }
            return timestamp;
        }

        // 获取当前的时间戳(10ms)
        protected virtual long TimestampGen()
        {
            return TimeExtensions.CurrentTimeTenMs();
        }
    }
}
