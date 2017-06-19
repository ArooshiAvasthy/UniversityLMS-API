using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class VideoDAL
    {
        University2Entities1 obj = new University2Entities1();

        public List<Video> GetAllVideos()
        {
            try
            {
                var data = (from c in obj.Videos
                            select c).ToList<Video>();

                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Video> GetSpecificVideos(string subject)
        {
            try
            {
                int id = Convert.ToInt32((from d in obj.Courses
                                          where d.CourseName == subject
                                          select d.CourseID).SingleOrDefault());
                var data = (from c in obj.Videos
                            where c.CourseID == id
                            select c).ToList<Video>();

                return data;
            }

            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
