using WebAPI_v3.Data;
using WebAPI_v3.Models;
using WebAPI_v3.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.Json;
using System.Reflection;
using System.ComponentModel.DataAnnotations;


namespace WebAPI_v3.Models
{
    public class Publishers
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public List<Book> Books { get; set; }
    }
}
