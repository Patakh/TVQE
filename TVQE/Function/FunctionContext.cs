using Npgsql;
using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Data;
using TVQE.Context;
using static Function.Tickets;

namespace Function;

public static class Tickets 
{
    public class Ticket
    {
        public long Id { get; set; }
        public string TicketNumberFull { get; set; }
        public string WindowName { get; set; }
    }

    public static List<Ticket> SelectTicketServed(long officeScoreboardId)
    {
       List<Ticket> ticketList = new List<Ticket>();
        try
        {
            // Создаем подключение к базе данных
            using (NpgsqlConnection connection = new NpgsqlConnection("Server=176.113.83.242;User Id=postgres;Password=!ShamiL19;Port=5432;Database=EQ"))
            {
                connection.Open();
                // Создаем команду для вызова функции и получения данных
                using (NpgsqlCommand command = new NpgsqlCommand("SELECT * FROM public.select_ticket_served(@in_s_office_scoreboard_id)", connection))
                {
                    command.Parameters.AddWithValue("in_s_office_scoreboard_id", NpgsqlDbType.Bigint, officeScoreboardId);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Ticket ticket = new Ticket();
                            ticket.Id = (long)(reader["out_d_ticket_id"] as long?);
                            ticket.TicketNumberFull = reader["out_ticket_number_full"] as string;
                            ticket.WindowName = reader["out_window_name"] as string;
                            ticketList.Add(ticket);
                        }
                    }
                }
            }
            return ticketList;
        }
        catch (Exception ex)
        {
            return ticketList;
        }
    }
}