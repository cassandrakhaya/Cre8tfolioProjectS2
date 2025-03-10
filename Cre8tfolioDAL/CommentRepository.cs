using Cre8tfolioBLL.Interfaces;
using Cre8tfolioBLL.Dto;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
//foutafhandeling hier aan toevoegen. Maar bedenk wel welke extra informatie je eraan wilt toevoegen.

namespace Cre8tfolioDAL
{
    public class CommentRepository : ICommentRepository
    {
        private readonly string Str;

            public CommentRepository(IConfiguration configuration)
            {
            // Get the connection string from appsettings.json
                Str = configuration.GetConnectionString("Cre8tfolioDatabase");

            }
        public virtual List<CommentDTO> GetCommentsByPostId(int portfolioPostId)
        {
            var comments = new List<CommentDTO>();

            using (var conn = new SqlConnection(Str))
            {
                conn.Open();
                using (var cmd = new SqlCommand("SELECT Id, PortfolioPostId, Content, Author FROM Comment WHERE PortfolioPostId = @PortfolioPostId", conn))
                {
                    cmd.Parameters.AddWithValue("@PortfolioPostId", portfolioPostId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            comments.Add(new CommentDTO
                            {
                                Id = reader.GetInt32(0),
                                PortfolioPostId = reader.GetInt32(1),
                                Content = reader.GetString(2),
                                Author = reader.GetString(3),
                            });
                        }
                    }
                }
            }

            return comments;
        }

        public virtual void AddComment(CommentDTO comment)
        {
            using (var conn = new SqlConnection(Str))
            {
                conn.Open();
                // Database maakt zelf een Id aan omdat het als een identity column configured is
                using (var cmd = new SqlCommand("INSERT INTO Comment (PortfolioPostId, Content, Author) VALUES (@PortfolioPostId, @Content, @Author)", conn))
                {
                    cmd.Parameters.AddWithValue("@PortfolioPostId", comment.PortfolioPostId);
                    cmd.Parameters.AddWithValue("@Content", comment.Content);
                    cmd.Parameters.AddWithValue("@Author", comment.Author);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public virtual void DeleteComment(int commentId)
        {
            using (var conn = new SqlConnection(Str))
            {
                conn.Open();
                using (var cmd = new SqlCommand("DELETE FROM Comment WHERE Id = @Id", conn))
                {
                    cmd.Parameters.AddWithValue("@Id", commentId);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
