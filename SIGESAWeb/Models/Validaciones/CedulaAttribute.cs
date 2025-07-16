using System;
using System.ComponentModel.DataAnnotations;

namespace SigesaWeb.Models.Validaciones
{
    public class CedulaAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
            {
                return new ValidationResult("La cédula es requerida.");
            }

            string cedula = value.ToString();

            if (!ValidaCedula(cedula))
            {
                return new ValidationResult("La cédula ingresada no es válida.");
            }

            return ValidationResult.Success;
        }

        private bool ValidaCedula(string cedula)
        {
            int verificador = 0;
            int digito = 0;
            int digitoVerificador = 0;
            int digitoImpar = 0;
            int sumaPar = 0;
            int sumaImpar = 0;
            int longitud = cedula.Length;

            try
            {
                // Verificamos que la longitud del parámetro sea igual a 11
                if (longitud == 11)
                {
                    digitoVerificador = Convert.ToInt32(cedula.Substring(10, 1));
                    // Recorremos en un ciclo for cada dígito de la cédula
                    for (int i = 9; i >= 0; i--)
                    {
                        digito = Convert.ToInt32(cedula.Substring(i, 1));
                        if ((i % 2) != 0)
                        {
                            digitoImpar = digito * 2;
                            // Si el dígito obtenido es mayor a 10, restamos 9
                            if (digitoImpar > 10)
                            {
                                digitoImpar -= 9;
                            }
                            sumaImpar += digitoImpar;
                        }
                        else
                        {
                            sumaPar += digito;
                        }
                    }
                    // Obtenemos el verificador restando a 10 el módulo 10 de la suma total de los dígitos
                    verificador = 10 - ((sumaPar + sumaImpar) % 10);
                    // Si el verificador es igual a 10 y el dígito verificador es igual a cero o el verificador y el dígito verificador son iguales, retorna verdadero
                    if ((verificador == 10 && digitoVerificador == 0) || (verificador == digitoVerificador))
                    {
                        return true;
                    }
                }
                else
                {
                    ErrorMessage = "La cédula debe contener once (11) dígitos.";
                }
            }
            catch
            {
                ErrorMessage = "No se pudo validar la cédula.";
            }
            return false;
        }
    }
}
