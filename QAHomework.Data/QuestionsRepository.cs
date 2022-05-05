using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QAHomework.Data
{
    public class QuestionsRepository
    {
        private readonly string _connectionString;

        public QuestionsRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        private Tag GetTag(string name)
        {
            using var context = new QuestionDataContext(_connectionString);
            return context.Tags.FirstOrDefault(t => t.Name == name);
        }

        private int AddTag(string name)
        {
            using var context = new QuestionDataContext(_connectionString);
            Tag tag = new Tag
            {
                Name = name
            };
            context.Add(tag);
            context.SaveChanges();
            return tag.Id;
        }

        public List<Question> GetQuestionsForTag(string name)
        {
            using var context = new QuestionDataContext(_connectionString);
            return context.Questions.Include(q => q.QuestionsTags).ThenInclude(qt => qt.Tag).Where(c => c.QuestionsTags.Any(t => t.Tag.Name == name)).ToList();
        }

        public List<Question> GetAllQuestions()
        {
            using var context = new QuestionDataContext(_connectionString);
            return context.Questions.Include(q => q.QuestionsTags).ThenInclude(n => n.Tag).Include(a => a.Answers).Include(l => l.Likes).OrderByDescending(d => d.DatePosted).ToList();
        }

        public void AddQuestionWithTags(Question question, IEnumerable<string> tags)
        {
            QuestionDataContext context = new QuestionDataContext(_connectionString);
            context.Questions.Add(question);
            
            context.SaveChanges();
            foreach(string tag in tags)
            {
                Tag newTag = GetTag(tag);
                int tagId;
                if (newTag == null)
                {
                    tagId = AddTag(tag);
                }
                else
                {
                    tagId = newTag.Id;
                }
                context.QuestionsTags.Add(new QuestionsTags
                {
                    QuestionId = question.Id,
                    TagId = tagId
                });

            }

            context.SaveChanges();
        }
    }
}
