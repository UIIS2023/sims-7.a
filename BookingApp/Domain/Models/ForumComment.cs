using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Serializer;

namespace BookingApp.Domain.Models
{
    public class ForumComment:ISerializable
    {
        public int id { get; set; }
        public ForumThread forumThread { get; set; }
        public User user { get; set; }
        public string comment { get; set; }
        public string commentTitle { get; set; }
        public DateTime commentDate { get; set; }
        public bool isHighlighted { get; set; }


        public ForumComment()
        {
            forumThread = new ForumThread();
            user = new User();
      
        }

        public ForumComment(ForumThread forumThread,User user,string comment,DateTime commentDate,string commentTitle,bool isHighlighted)
        {
            this.forumThread = forumThread;
            this.user = user;
            this.comment = comment;
            this.commentDate = commentDate;
            this.commentTitle = commentTitle;
            this.isHighlighted = isHighlighted;
        }

        public string[] ToCSV()
        {
            string[] csvValues =
                    {
                        id.ToString(),
                        forumThread.id.ToString(),
                        user.id.ToString(),
                        comment,
                        commentDate.ToString("dd'/'MM'/'yyyy HH':'mm"),
                        commentTitle,
                        isHighlighted.ToString()
                    };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            id = int.Parse(values[0]);
            forumThread.id = int.Parse(values[1]);
            user.id = int.Parse(values[2]);
            comment = values[3];
            commentDate = DateTime.ParseExact(values[4], "dd'/'MM'/'yyyy HH':'mm",CultureInfo.InvariantCulture);
            commentTitle = values[5];
            isHighlighted = bool.Parse(values[6]);
        }
    }
}
