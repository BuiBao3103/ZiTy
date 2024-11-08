﻿using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.UserAnswers;

public class UserAnswerUpdateDTO
{
    [Required]
    public int? AnswerId { get; set; }
    [Required]
    public int? UserId { get; set; }
}
