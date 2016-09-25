using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFXIVDeviare.Utility
{
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Net;
    using System.Runtime.InteropServices;
    public class TcpHelper
    {
        public enum TCP_TABLE_CLASS : int
        {
            TCP_TABLE_BASIC_LISTENER,
            TCP_TABLE_BASIC_CONNECTIONS,
            TCP_TABLE_BASIC_ALL,
            TCP_TABLE_OWNER_PID_LISTENER,
            TCP_TABLE_OWNER_PID_CONNECTIONS,
            TCP_TABLE_OWNER_PID_ALL,
            TCP_TABLE_OWNER_MODULE_LISTENER,
            TCP_TABLE_OWNER_MODULE_CONNECTIONS,
            TCP_TABLE_OWNER_MODULE_ALL
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct MIB_TCPROW_OWNER_PID
        {
            public MibTcpState state;
            public uint localAddr;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] localPort;
            public uint remoteAddr;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] remotePort;
            public int owningPid;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct MIB_TCPTABLE_OWNER_PID
        {
            public uint dwNumEntries;
            MIB_TCPROW_OWNER_PID table;
        }

        [DllImport("iphlpapi.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern uint GetExtendedTcpTable(IntPtr pTcpTable, ref int pdwSize,
            bool bOrder, int ulAf, TcpTableClass tableClass, uint reserved = 0);

        public enum MibTcpState
        {
            CLOSED = 1,
            LISTENING = 2,
            SYN_SENT = 3,
            SYN_RCVD = 4,
            ESTABLISHED = 5,
            FIN_WAIT1 = 6,
            FIN_WAIT2 = 7,
            CLOSE_WAIT = 8,
            CLOSING = 9,
            LAST_ACK = 10,
            TIME_WAIT = 11,
            DELETE_TCB = 12,
            NONE = 0
        }
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public class TcpProcessRecord
        {
            [DisplayName("Local Address")]
            public IPAddress LocalAddress { get; set; }
            [DisplayName("Local Port")]
            public ushort LocalPort { get; set; }
            [DisplayName("Remote Address")]
            public IPAddress RemoteAddress { get; set; }
            [DisplayName("Remote Port")]
            public ushort RemotePort { get; set; }
            [DisplayName("State")]
            public MibTcpState State { get; set; }
            [DisplayName("Process ID")]
            public int ProcessId { get; set; }
            [DisplayName("Process Name")]
            public string ProcessName { get; set; }

            public TcpProcessRecord(IPAddress localIp, IPAddress remoteIp, ushort localPort,
                ushort remotePort, int pId, MibTcpState state)
            {
                LocalAddress = localIp;
                RemoteAddress = remoteIp;
                LocalPort = localPort;
                RemotePort = remotePort;
                State = state;
                ProcessId = pId;
                // Getting the process name associated with a process id.
                if (Process.GetProcesses().Any(process => process.Id == pId))
                {
                    ProcessName = Process.GetProcessById(ProcessId).ProcessName;
                }
            }
        }
        public enum TcpTableClass
        {
            TCP_TABLE_BASIC_LISTENER,
            TCP_TABLE_BASIC_CONNECTIONS,
            TCP_TABLE_BASIC_ALL,
            TCP_TABLE_OWNER_PID_LISTENER,
            TCP_TABLE_OWNER_PID_CONNECTIONS,
            TCP_TABLE_OWNER_PID_ALL,
            TCP_TABLE_OWNER_MODULE_LISTENER,
            TCP_TABLE_OWNER_MODULE_CONNECTIONS,
            TCP_TABLE_OWNER_MODULE_ALL
        }

        private const int AF_INET = 2;
        public static List<TcpProcessRecord> GetAllTcpConnections()
        {
            int bufferSize = 0;
            List<TcpProcessRecord> tcpTableRecords = new List<TcpProcessRecord>();

            // Getting the size of TCP table, that is returned in 'bufferSize' variable. 
            uint result = GetExtendedTcpTable(IntPtr.Zero, ref bufferSize, true, AF_INET,
                TcpTableClass.TCP_TABLE_OWNER_PID_ALL);

            // Allocating memory from the unmanaged memory of the process by using the 
            // specified number of bytes in 'bufferSize' variable. 
            IntPtr tcpTableRecordsPtr = Marshal.AllocHGlobal(bufferSize);

            try
            {
                // The size of the table returned in 'bufferSize' variable in previous 
                // call must be used in this subsequent call to 'GetExtendedTcpTable' 
                // function in order to successfully retrieve the table. 
                result = GetExtendedTcpTable(tcpTableRecordsPtr, ref bufferSize, true,
                    AF_INET, TcpTableClass.TCP_TABLE_OWNER_PID_ALL);

                // Non-zero value represent the function 'GetExtendedTcpTable' failed, 
                // hence empty list is returned to the caller function. 
                if (result != 0)
                    return new List<TcpProcessRecord>();

                // Marshals data from an unmanaged block of memory to a newly allocated 
                // managed object 'tcpRecordsTable' of type 'MIB_TCPTABLE_OWNER_PID' 
                // to get number of entries of the specified TCP table structure. 
                MIB_TCPTABLE_OWNER_PID tcpRecordsTable = (MIB_TCPTABLE_OWNER_PID)
                                        Marshal.PtrToStructure(tcpTableRecordsPtr,
                                        typeof(MIB_TCPTABLE_OWNER_PID));
                IntPtr tableRowPtr = (IntPtr)((long)tcpTableRecordsPtr +
                                        Marshal.SizeOf(tcpRecordsTable.dwNumEntries));

                // Reading and parsing the TCP records one by one from the table and 
                // storing them in a list of 'TcpProcessRecord' structure type objects. 
                for (int row = 0; row < tcpRecordsTable.dwNumEntries; row++)
                {
                    MIB_TCPROW_OWNER_PID tcpRow = (MIB_TCPROW_OWNER_PID)Marshal.
                        PtrToStructure(tableRowPtr, typeof(MIB_TCPROW_OWNER_PID));
                    tcpTableRecords.Add(new TcpProcessRecord(
                                          new IPAddress(tcpRow.localAddr),
                                          new IPAddress(tcpRow.remoteAddr),
                                          BitConverter.ToUInt16(new byte[2] {
                                              tcpRow.localPort[1],
                                              tcpRow.localPort[0] }, 0),
                                          BitConverter.ToUInt16(new byte[2] {
                                              tcpRow.remotePort[1],
                                              tcpRow.remotePort[0] }, 0),
                                          tcpRow.owningPid, tcpRow.state));
                    tableRowPtr = (IntPtr)((long)tableRowPtr + Marshal.SizeOf(tcpRow));
                }
            }
            catch (OutOfMemoryException)
            {

            }
            catch (Exception)
            {

            }
            finally
            {
                Marshal.FreeHGlobal(tcpTableRecordsPtr);
            }
            return tcpTableRecords != null ? tcpTableRecords.Distinct()
                .ToList<TcpProcessRecord>() : new List<TcpProcessRecord>();
        }
    }
}