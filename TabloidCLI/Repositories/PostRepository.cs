﻿using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using TabloidCLI.Models;
using TabloidCLI.Repositories;

namespace TabloidCLI
{
    public class PostRepository : DatabaseConnector, IRepository<Post>
    {

        public PostRepository(string connectionString) : base(connectionString) { }

        public List<Post> GetAll()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT p.id,
                                               p.Title As PostTitle,
                                               p.URL AS PostUrl,
                                               p.PublishDateTime,
                                               p.AuthorId,
                                               p.BlogId,
                                               a.FirstName,
                                               a.LastName,
                                               a.Bio,
                                               b.Title AS BlogTitle,
                                               b.URL AS BlogUrl
                                          FROM Post p 
                                               LEFT JOIN Author a on p.AuthorId = a.Id
                                               LEFT JOIN Blog b on p.BlogId = b.Id";

                    List<Post> posts = new List<Post>();
                    
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                       
                    {
                        Author author = new Author()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("AuthorId")),
                            FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                            LastName = reader.GetString(reader.GetOrdinal("LastName"))
                        };

                        Blog blog = new Blog()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("BlogId")),
                            Title = reader.GetString(reader.GetOrdinal("BlogTitle"))
                        };

                        Post post = new Post()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Title = reader.GetString(reader.GetOrdinal("PostTitle")),
                            Url = reader.GetString(reader.GetOrdinal("PostUrl")),
                            PublishDateTime = reader.GetDateTime(reader.GetOrdinal("PublishDateTime")),
                            Author = author,
                            Blog = blog,
                        };
                        posts.Add(post);
                    };
                    reader.Close();
                    return posts;
                }
            }
        }

        public Post Get(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT p.id,
                                               p.Title As PostTitle,
                                               p.URL AS PostUrl,
                                               p.PublishDateTime,
                                               p.AuthorId,
                                               p.BlogId,
                                               a.FirstName,
                                               a.LastName,
                                               a.Bio,
                                               b.Title AS BlogTitle,
                                               b.URL AS BlogUrl
                                          FROM Post p 
                                               LEFT JOIN Author a on p.AuthorId = a.Id
                                               LEFT JOIN Blog b on p.BlogId = b.Id 
                                         WHERE p.Id = @id";

                    cmd.Parameters.AddWithValue("@id", id);

                    Post post = null;

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        if (post == null)
                        {
                            Author author = new Author()
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("AuthorId")),
                                FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                                LastName = reader.GetString(reader.GetOrdinal("LastName"))
                            };

                            Blog blog = new Blog()
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("BlogId")),
                                Title = reader.GetString(reader.GetOrdinal("BlogTitle"))
                            };


                            post = new Post()
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                Title = reader.GetString(reader.GetOrdinal("Title")),
                                Url = reader.GetString(reader.GetOrdinal("Url")),
                                PublishDateTime = reader.GetDateTime(reader.GetOrdinal("PublishDateTime")),
                                Author = author,
                                Blog = blog,
                            };
                        }

                        reader.Close();
                    }
                    return post;
                }
            }
        }

        internal List<Post> GetByAuthor(int authorId)
        {
            throw new NotImplementedException();
        }

        public void Insert(Post post)
                {
                    using (SqlConnection conn = Connection)
                    {
                        conn.Open();
                        using (SqlCommand cmd = conn.CreateCommand())
                        {
                            cmd.CommandText = @"INSERT INTO Post (Title, Url, PublishDateTime, AuthorId, BlogId )
                                                         VALUES (@title, @url, @publishdatetime, @authorId, @blogId)";
                            cmd.Parameters.AddWithValue("@title", post.Title);
                            cmd.Parameters.AddWithValue("@url", post.Url);
                            cmd.Parameters.AddWithValue("@authorId", post.Author.Id);
                            cmd.Parameters.AddWithValue("@publishdatetime", post.PublishDateTime);
                            cmd.Parameters.AddWithValue("@blogId", post.Blog.Id);

                            cmd.ExecuteNonQuery();
                        }
                    }
                }

                public void Update(Post post)
                {
                    using (SqlConnection conn = Connection)
                    {
                        conn.Open();
                        using (SqlCommand cmd = conn.CreateCommand())
                        {
                            cmd.CommandText = @"UPDATE Post 
                                               SET Title = @title,
                                                   Url = @url,
                                                   PublishDateTime = @publishDateTime,
                                                   AuthorId = @authorId,
                                                   BlogId = @blogId
                                             WHERE id = @id";

                            cmd.Parameters.AddWithValue("@id", post.Id);        
                            cmd.Parameters.AddWithValue("@title", post.Title);
                            cmd.Parameters.AddWithValue("@url", post.Url);
                            cmd.Parameters.AddWithValue("@authorId", post.Author.Id);
                            cmd.Parameters.AddWithValue("@publishDateTime", post.PublishDateTime);
                            cmd.Parameters.AddWithValue("@blogId", post.Blog.Id);

                            cmd.ExecuteNonQuery();
                        }
                    }
                }

                public void Delete(int id)
                {
                    using (SqlConnection conn = Connection)
                    {
                        conn.Open();
                        using (SqlCommand cmd = conn.CreateCommand())
                        {
                            cmd.CommandText = @"DELETE FROM Post WHERE id = @id";
                            cmd.Parameters.AddWithValue("@id", id);

                            cmd.ExecuteNonQuery();
                        }
                    }
                }



    }
}
