using System;
using System.ComponentModel.DataAnnotations;

namespace CorsoCoreGabriel.Models.ViewModels
{

public class CourseViewModel
{
    [Key]
    public int Id { get; set; }
    public string Title { get; set; }
    public string ImagePath { get; set; }
    public string Author { get; set; }
    public double Rating { get; set; }
    public double FullPrice { get; set; }
    public double CurrentPrice { get; set; }


}

}