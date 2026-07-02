using System;
using System.ComponentModel.DataAnnotations;

namespace TodoAppMVC.Models
{
    public class Todo
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Lütfen bir görev başlığı giriniz.")]
        public string? Title { get; set; }

        public bool IsCompleted { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
