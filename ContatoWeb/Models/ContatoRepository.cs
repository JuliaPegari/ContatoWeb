using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ContatoWeb.Models
{
    public class ContatoRepository : AbstractRepository<Contato, int>
    {
        public override void Delete(Contato entity)
        {
            using (var conn = new NpgsqlConnection(StringConn))
            {
                string sql = "DELETE FROM tb_contato WHERE id=@id";
                NpgsqlCommand cmd = new NpgsqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", entity.Id);
                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }

        public override void DeleteById(int id)
        {
            throw new NotImplementedException();
        }

        public override List<Contato> GetAll()
        {
           using (var conn = new NpgsqlConnection(StringConn))
           {
                string sql = "SELECT * FROM tb_contato";
                NpgsqlCommand cmd = new NpgsqlCommand(sql, conn);
                List<Contato> list = new List<Contato>();
                Contato c = null;
                try
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader
                        (CommandBehavior.CloseConnection))
                    { 
                        while(reader.Read())
                        {
                            c = new Contato();
                            c.Id = (int)reader["id"];
                            c.Nome = reader["nome"].ToString();
                            c.Email = reader["email"].ToString();
                            c.Telefone = reader["telefone"].ToString();
                            list.Add(c);
                        }
                    }
                } catch (Exception e)
                {
                    throw e;
                }
                return list;
            }
        }

        public override Contato GetById(int id)
        {
            using (var conn = new NpgsqlConnection(StringConn))
            {
                string sql = "SELECT * FROM tb_contato WHERE id=@id";
                NpgsqlCommand cmd = new NpgsqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", id);
                Contato c = null;
                try
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader
                        (CommandBehavior.CloseConnection))
                    {
                        if(reader.HasRows)
                        {
                            if(reader.Read())
                            {
                                c = new Contato();
                                c.Id = (int)reader["id"];
                                c.Nome = reader["nome"].ToString();
                                c.Email = reader["email"].ToString();
                                c.Telefone = reader["telefone"].ToString();
                            }
                        }
                    }
                } catch (Exception e)
                {
                    throw e;
                }
                return c;
            }
        }

        public override void Save(Contato entity)
        {
            using (var conn = new NpgsqlConnection(StringConn))
            {
                string sql = "INSERT INTO tb_contato (nome, email, telefone)" +
                             "VALUES (@nome, @email, @telefone);";
                NpgsqlCommand cmd = new NpgsqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@nome", entity.Nome);
                cmd.Parameters.AddWithValue("@email", entity.Email);
                cmd.Parameters.AddWithValue("@telefone", entity.Telefone);

                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }

        public override void Update(Contato entity)
        {
            using (var conn = new NpgsqlConnection(StringConn))
            {
                string sql = "UPDATE tb_contato SET nome=@nome, email=@email, telefone=@telefone " +
                    "WHERE id=@id";
                NpgsqlCommand cmd = new NpgsqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", entity.Id);
                cmd.Parameters.AddWithValue("@nome", entity.Nome);
                cmd.Parameters.AddWithValue("@email", entity.Email);
                cmd.Parameters.AddWithValue("@telefone", entity.Telefone);
                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                } catch (Exception e)
                {
                    throw e;
                }
            }
        }
    }
}