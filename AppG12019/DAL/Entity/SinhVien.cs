﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppG12019.DAL.Entity
{
    public class SinhVien
    {
        public string maSinhVien { get; set; }
        public string ho { get; set; }
        public string ten { get; set; }
        public DateTime ngaySinh { get; set; }
        public SEX gioiTinh { get; set; }
        public string queQuan { get; set; }
        public virtual ICollection<QuaTrinhHocTap> ListQuaTrinhHocTap { get; set; }

        /// <summary>
        /// lấy sinh viên từ Mock data
        /// </summary>
        /// <param name="maSinhVien"></param>
        /// <returns></returns>
        public static SinhVien get(string maSinhVien)
        {
            SinhVien sinhVien = new SinhVien
            {
                maSinhVien = maSinhVien,
                ho = "Nguyen",
                ten = "Duc",
                ngaySinh = DateTime.Now.Date,
                gioiTinh = SEX.Male,
                queQuan = "TTHue"
            };
            sinhVien.ListQuaTrinhHocTap = QuaTrinhHocTap.getList(maSinhVien);
            return sinhVien;
        }

        /// <summary>
        /// lấy sinh viên từ file dataStudent.txt
        /// </summary>
        /// <param name="maSinhVien"></param>
        /// <returns></returns>
        public static SinhVien getFromFile(string path, string maSinhVien)
        {
            string pathLearningHistory = Application.StartupPath + @"\DATA\learningHistory.txt";
            var arrayLines = File.ReadAllLines(path);
            CultureInfo cultureInfo = CultureInfo.InvariantCulture;
            foreach (var line in arrayLines)
            {
                if (line.Contains(maSinhVien))
                {
                    var lstValue = line.Split(new char[] { '#' });
                    var sinhVien = new SinhVien
                    {
                        maSinhVien = lstValue[0],
                        ho = lstValue[1],
                        ten = lstValue[2],
                        gioiTinh = lstValue[3] == "Male" ? SEX.Male : (lstValue[3] == "Female" ? SEX.Female : SEX.Other),
                        ngaySinh = DateTime.ParseExact(lstValue[4], "yyyy-MM-dd", cultureInfo),
                        queQuan = lstValue[5]
                    };
                    sinhVien.ListQuaTrinhHocTap = QuaTrinhHocTap.getListFormFile(pathLearningHistory, maSinhVien);
                    return sinhVien;
                }
            }
            return null;
        }
    }
}


public enum SEX
{
    Female, Male, Other
}

