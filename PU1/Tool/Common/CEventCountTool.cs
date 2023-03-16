using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PU1
{
    public class CEventCountTool
    {
        #region CONST
        private const long TICK_ON_HOUR = TICK_ON_MINUTE * 60;
        private const long TICK_ON_MINUTE = TICK_ON_SECOND * 60;
        private const long TICK_ON_SECOND = TICK_ON_MILLISECOND * 1000;
        private const long TICK_ON_MILLISECOND = 10000;
        #endregion


        #region VARIABLE
        private List<long> m_LstI64Count = null;
        private object m_OInterrupt = null;
        #endregion


        #region CONSTRUCTOR & DESTRUCTOR
        public CEventCountTool()
        {
            try
            {
                this.m_LstI64Count = new List<long>();
                this.m_OInterrupt = new object();
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
        }
        #endregion


        #region FUNCTION
        /// <summary>
        /// 이벤트 발생 수를 증가시킨다.
        /// </summary>
        public void AddCount()
        {
            try
            {
                Monitor.Enter(this.m_OInterrupt);

                this.m_LstI64Count.Add(DateTime.Now.Ticks);
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
            finally
            {
                Monitor.Exit(this.m_OInterrupt);
            }
        }


        /// <summary>
        /// 전체 이벤트 발생 수를 반환한다.
        /// </summary>
        /// <returns>전체 이벤트 발생 수</returns>
        public int OnTotal()
        {
            int I32Result = 0;

            try
            {
                Monitor.Enter(this.m_OInterrupt);

                I32Result = this.m_LstI64Count.Count;
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
            finally
            {
                Monitor.Exit(this.m_OInterrupt);
            }

            return I32Result;
        }


        /// <summary>
        /// 최근 1시간 동안의 이벤트 발생 수를 반환한다.
        /// </summary>
        /// <returns>최근 1시간 동안의 이벤트 발생 수</returns>
        public int OnHour()
        {
            int I32Result = 0;

            try
            {
                Monitor.Enter(this.m_OInterrupt);

                long I64Tick = DateTime.Now.Ticks - TICK_ON_HOUR;

                for (int _Index = this.m_LstI64Count.Count - 1; _Index >= 0; _Index++)
                {
                    if (this.m_LstI64Count[_Index] >= I64Tick) continue;

                    I32Result = this.m_LstI64Count.Count - (_Index + 1);
                    break;
                }
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
            finally
            {
                Monitor.Exit(this.m_OInterrupt);
            }

            return I32Result;
        }


        /// <summary>
        /// <returns>최근 1분 동안의 이벤트 발생 수</returns>
        /// 최근 1분  이벤트 발생 수를 반환한다.
        /// </summary>
        /// <returns>최근 1분 동안의 이벤트 발생 수</returns>
        public int OnMinute()
        {
            int I32Result = 0;

            try
            {
                Monitor.Enter(this.m_OInterrupt);

                long I64Tick = DateTime.Now.Ticks - TICK_ON_MINUTE;

                for (int _Index = this.m_LstI64Count.Count - 1; _Index >= 0; _Index++)
                {
                    if (this.m_LstI64Count[_Index] >= I64Tick) continue;

                    I32Result = this.m_LstI64Count.Count - (_Index + 1);
                    break;
                }
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
            finally
            {
                Monitor.Exit(this.m_OInterrupt);
            }

            return I32Result;
        }


        /// <summary>
        /// 최근 1초 동안의 이벤트 발생 수를 반환한다.
        /// </summary>
        /// <returns>최근 1초 동안의 이벤트 발생 수</returns>
        public int OnSecond()
        {
            int I32Result = 0;

            try
            {
                Monitor.Enter(this.m_OInterrupt);

                long I64Tick = DateTime.Now.Ticks - TICK_ON_SECOND;

                for (int _Index = this.m_LstI64Count.Count - 1; _Index >= 0; _Index++)
                {
                    if (this.m_LstI64Count[_Index] >= I64Tick) continue;

                    I32Result = this.m_LstI64Count.Count - (_Index + 1);
                    break;
                }
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
            finally
            {
                Monitor.Exit(this.m_OInterrupt);
            }

            return I32Result;
        }


        /// <summary>
        /// 최근 1밀리초 동안의 이벤트 발생 수를 반환한다.
        /// </summary>
        /// <returns>최근 1밀리초 동안의 이벤트 발생 수</returns>
        public int OnMilliSecond()
        {
            int I32Result = 0;

            try
            {
                Monitor.Enter(this.m_OInterrupt);

                long I64Tick = DateTime.Now.Ticks - TICK_ON_MILLISECOND;

                for (int _Index = this.m_LstI64Count.Count - 1; _Index >= 0; _Index++)
                {
                    if (this.m_LstI64Count[_Index] >= I64Tick) continue;

                    I32Result = this.m_LstI64Count.Count - (_Index + 1);
                    break;
                }
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
            finally
            {
                Monitor.Exit(this.m_OInterrupt);
            }

            return I32Result;
        }


        /// <summary>
        /// 모든 이벤트 발생 수를 제거한다.
        /// </summary>
        public void Reset()
        {
            try
            {
                Monitor.Enter(this.m_OInterrupt);

                this.m_LstI64Count.Clear();
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
            finally
            {
                Monitor.Exit(this.m_OInterrupt);
            }
        }


        /// <summary>
        /// (현재시간 - 입력시간) 이전의 이벤트 발생 정보를 제거한다.
        /// </summary>
        /// <param name="I32Hour">시</param>
        /// <param name="I32Minute">분</param>
        /// <param name="I32Second">초</param>
        public void Reset(int I32Hour, int I32Minute, int I32Second, int I32MilliSecond)
        {
            try
            {
                Monitor.Enter(this.m_OInterrupt);

                long I64Tick = DateTime.Now.Ticks;
                I64Tick -= (I32Hour * TICK_ON_HOUR);
                I64Tick -= (I32Minute * TICK_ON_MINUTE);
                I64Tick -= (I32Second * TICK_ON_SECOND);
                I64Tick -= (I32MilliSecond * TICK_ON_MILLISECOND);

                while (this.m_LstI64Count.Count > 0)
                {
                    if (this.m_LstI64Count[0] < I64Tick)
                    {
                        this.m_LstI64Count.RemoveAt(0);
                    }
                    else break;
                }
            }
            catch (Exception ex)
            {
                CError.Throw(ex);
            }
            finally
            {
                Monitor.Exit(this.m_OInterrupt);
            }
        }
        #endregion
    }
}
