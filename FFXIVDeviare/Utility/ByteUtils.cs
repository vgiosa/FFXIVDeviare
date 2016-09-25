using FFXIVDeviare.Packets.Subpackets;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace FFXIVDeviare.Utility
{
    public static class ByteUtils
    {

        public static byte[] ConvertStructToByteArray(Object strct)
        {
            int size = Marshal.SizeOf(strct);
            byte[] arr = new byte[size];
            IntPtr ptr = Marshal.AllocHGlobal(size);
            Marshal.StructureToPtr(strct, ptr, true);
            Marshal.Copy(ptr, arr, 0, size);
            Marshal.FreeHGlobal(ptr);
            return arr;
            
        }



        public static void PrintByteArray(byte[] bytes)
        {
            var sb = new StringBuilder("new byte[]  ");
            foreach (var b in bytes)
            {
                sb.Append(b.ToString("X2") + " ");
            }

            Debug.WriteLine(sb.ToString());
        }

        public static string ByteArrayToString(byte[] bytes)
        {
            var sb = new StringBuilder("");
            foreach (var b in bytes)
            {
                sb.Append(b.ToString("X2") + " ");
            }

            return sb.ToString();
        }


        public static string StructToString(Object o)
        {

            Type oType = o.GetType();
            StringBuilder output = new StringBuilder();
            foreach (FieldInfo field in oType.GetFields())
            {
                output.Append(field.Name);
                output.Append(": ");

                var attr = field.GetCustomAttributes(typeof(FixedBufferAttribute), false);
                if (attr.Length > 0) { 
                    FixedBufferAttribute nameAttr = (FixedBufferAttribute)attr[0];
                    GCHandle hdl = GCHandle.Alloc(field.GetValue(o), GCHandleType.Pinned);
                    string test = Marshal.PtrToStringAnsi(hdl.AddrOfPinnedObject());
                    output.Append(test);
                }
                else { 
                    output.Append(field.GetValue(o));
                }
                output.Append(", ");
            }

            foreach (PropertyInfo field in oType.GetProperties())
            {
                output.Append(field.Name);
                output.Append(": ");

                var attr = field.GetCustomAttributes(typeof(FixedBufferAttribute), false);
                if (attr.Length > 0)
                {
                    FixedBufferAttribute nameAttr = (FixedBufferAttribute)attr[0];
                    GCHandle hdl = GCHandle.Alloc(field.GetValue(o), GCHandleType.Pinned);
                    string test = Marshal.PtrToStringAnsi(hdl.AddrOfPinnedObject());
                    output.Append(test);
                }
                else {
                    output.Append(field.GetValue(o));
                }
                output.Append(", ");
            }

            return output.ToString() ;
        }
        public static DataTable ToDataTable<T>(this IList<T> data)
        {
            PropertyDescriptorCollection props =
                TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            for (int i = 0; i < props.Count; i++)
            {
                PropertyDescriptor prop = props[i];
                table.Columns.Add(prop.Name, prop.PropertyType);
            }
            object[] values = new object[props.Count];
            foreach (T item in data)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = props[i].GetValue(item);
                }
                table.Rows.Add(values);
            }
            return table;
        }

        public static byte[] GetByteArrayFromPacket(Packets.Packet p)
        {
            if (p.Subpackets == null || p.Subpackets.Count == 0) return null;
            byte[] bytes = new byte[0];
            byte[] header = ByteUtils.ConvertStructToByteArray(p.Header);

            foreach (Subpacket sp in p.Subpackets)
            {
                byte[] subheader = ByteUtils.ConvertStructToByteArray(sp.SubpacketHeader);
                Array.Copy(subheader, 0, bytes, bytes.Length, 0);
                if (sp.PacketData.GetType().Equals(typeof(List<Object>)))
                {
                    foreach (Object d in (List<Object>)sp.PacketData)
                    {
                        byte[] subbody = ByteUtils.ConvertStructToByteArray(d);
                        Array.Copy(subbody, 0, bytes, bytes.Length, 0);
                    }
                }
                else
                {
                    byte[] subbody = ByteUtils.ConvertStructToByteArray(sp.PacketData);
                    Array.Copy(subbody, 0, bytes, bytes.Length, 0);
                }
            }


            return bytes;

        }

        public static DataTable ToDataTable<T>(this IList<T> data, Type type)
        {
            PropertyDescriptorCollection props =
                TypeDescriptor.GetProperties(type);
            DataTable table = new DataTable();
            for (int i = 0; i < props.Count; i++)
            {
                PropertyDescriptor prop = props[i];
                table.Columns.Add(prop.Name, prop.PropertyType);
            }
            object[] values = new object[props.Count];
            foreach (T item in data)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = props[i].GetValue(item);
                }
                table.Rows.Add(values);
            }
            return table;
        }
    }
}
