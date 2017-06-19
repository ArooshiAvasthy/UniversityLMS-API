using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class CourseDAL
    {
        University2Entities1 obj = new University2Entities1();

        public List<Course> GetAllCourses()
        {
            try
            {
                var data = (from c in obj.Courses
                            select c).ToList<Course>();
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void EnrolledUser(string username, string coursename)
        {
            try
            {
                var usernameParam = new SqlParameter
                {
                    ParameterName = "username",
                    Value = username
                };
                var courseParam = new SqlParameter
                {
                    ParameterName = "coursename",
                    Value = coursename
                };

                var productList = obj.Enrollment2.SqlQuery("exec AddToEnrollment @username,@coursename", usernameParam, courseParam).ToList<Enrollment2>();
            
            }
           catch(Exception ex)
            {
                throw ex;
            }
        }

        public List<PDFFile> GetFiles(string courseName)
        {
            try
            {
                var files = (from c in obj.PDFFiles
                             where c.Courses == courseName
                             select c).ToList<PDFFile>();
                return files;
            }
          catch(Exception ex)
            {
                throw ex;
            }
        }


        public PDFFile GetSpecificFile(int id)
        {
            try
            {
                var files = (from c in obj.PDFFiles
                             where c.Id == id
                             select c).FirstOrDefault();
                return files;
            }
           
            catch(Exception ex)
            {
                throw ex;
            }

        }

        public void AddFile(PDFFile fileObj)
        {
            try
            {
                obj.PDFFiles.Add(fileObj);
                obj.SaveChanges();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        
    }
}
