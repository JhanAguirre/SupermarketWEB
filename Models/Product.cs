<<<<<<< HEAD
﻿using System.ComponentModel.DataAnnotations;
=======
﻿using System.ComponentModel.DataAnnotations.Schema;
>>>>>>> 2259ff3db980ad36a4da4cd4a04b056baa246be0

namespace SupermarketWEB.Models
{
    public class Product
    {
<<<<<<< HEAD
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es requerido")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "El nombre debe tener entre 3 y 50 caracteres")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "El precio es requerido")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El precio debe ser mayor a 0")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "El stock es requerido")]
        [Range(0, int.MaxValue, ErrorMessage = "El stock debe ser mayor o igual a 0")]
        public int Stock { get; set; }

        [Required(ErrorMessage = "La categoría es requerida")]
        public int CategoryId { get; set; }

        public Category? Category { get; set; }
    }
}
=======
        // [Key] -> Anotación si la propiedad no se llama Id, ejemplo ProductId
        public int Id { get; set; } // Será la llave primaria

        public string Name { get; set; }

        [Column(TypeName = "decimal(6,2)")]
        public decimal Price { get; set; }

        public int Stock { get; set; }

        public int CategoryId { get; set; } // Será la llave foranea

        public Category Category { get; set; } // Propiedad de navegación
    }
}
>>>>>>> 2259ff3db980ad36a4da4cd4a04b056baa246be0
