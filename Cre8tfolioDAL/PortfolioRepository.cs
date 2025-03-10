using System.Data.SqlClient;
using Cre8tfolioBLL.Interfaces;
using Microsoft.Extensions.Configuration;
using Cre8tfolioBLL.Dto;

namespace Cre8tfolioDAL
{
    public class PortfolioRepository : IPortfolioRepository
    {

        private readonly string Str;

        public PortfolioRepository(IConfiguration configuration)
        {
            // Get the connection string from appsettings.json
            Str = configuration.GetConnectionString("Cre8tfolioDatabase");
        }

        public List<PortfolioPostDTO> GetPortfolioPostDTOs()
        {
            List<PortfolioPostDTO> posts = new List<PortfolioPostDTO>();

            using (SqlConnection con = new SqlConnection(Str))
            {
                con.Open();

                string query = "SELECT Id, Title, Description, ImagePath FROM PortfolioPost";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        
                        while (reader.Read())
                        {
                            var dto = new PortfolioPostDTO
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                Title = reader.GetString(reader.GetOrdinal("Title")),
                                Description = reader.GetString(reader.GetOrdinal("Description")),
                                ImagePath = reader.IsDBNull(reader.GetOrdinal("ImagePath")) ? null : reader.GetString(reader.GetOrdinal("ImagePath")),
                                Comments = new List<CommentDTO>() // initialize the comments list

                            };

                            posts.Add(dto);
                        }
                    }
                }
            }

            return posts;
        }
        public PortfolioPostDTO GetPortfolioPostById(int id)
        {
            PortfolioPostDTO dto = null;

            using (SqlConnection con = new SqlConnection(Str))
            {
                con.Open();
                string query = "SELECT Id, Title, Description, ImagePath FROM PortfolioPost WHERE Id = @Id";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            dto = new PortfolioPostDTO
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                Title = reader.GetString(reader.GetOrdinal("Title")),
                                Description = reader.GetString(reader.GetOrdinal("Description")),
                                ImagePath = reader.IsDBNull(reader.GetOrdinal("ImagePath")) ? null : reader.GetString(reader.GetOrdinal("ImagePath")),
                                Comments = new List<CommentDTO>()

                            };
                        }
                    }
                }
            }

            return dto;
        }

        public void CreatePost(PortfolioPostDTO portfolioPost/*, string title, string description, string imagePath*/)
        {

            using (SqlConnection con = new SqlConnection(Str))
            {
                con.Open();

                string query = "INSERT INTO PortfolioPost (Title, Description, ImagePath) VALUES (@Title, @Description, @ImagePath)";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Title", portfolioPost.Title);
                    cmd.Parameters.AddWithValue("@Description", portfolioPost.Description);
                    cmd.Parameters.AddWithValue("@ImagePath", (object)portfolioPost.ImagePath ?? DBNull.Value);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeletePost(int id)
        {
            using (SqlConnection con = new SqlConnection(Str))
            {
                con.Open();

                string query = "DELETE FROM PortfolioPost WHERE Id = @Id";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public void EditPost(PortfolioPostDTO portfolioPost)
        {

            using (SqlConnection con = new SqlConnection(Str))
            {
                con.Open();
                if (portfolioPost.ImagePath != null)
                {
                    string query = "UPDATE PortfolioPost SET Title = @Title, Description = @Description, ImagePath = @ImagePath WHERE Id = @Id";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@Id", portfolioPost.Id);
                        cmd.Parameters.AddWithValue("@Title", portfolioPost.Title);
                        cmd.Parameters.AddWithValue("@Description", portfolioPost.Description);
                        cmd.Parameters.AddWithValue("@ImagePath", (object)portfolioPost.ImagePath ?? DBNull.Value);

                        cmd.ExecuteNonQuery();
                    }
                }
                else
                {
                    string query = "UPDATE PortfolioPost SET Title = @Title, Description = @Description WHERE Id = @Id";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@Id", portfolioPost.Id);
                        cmd.Parameters.AddWithValue("@Title", portfolioPost.Title);
                        cmd.Parameters.AddWithValue("@Description", portfolioPost.Description);

                        cmd.ExecuteNonQuery();
                    }
                }



            }
        }
    }
}

