using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Linq;

using Tiendita.Models;

namespace Tiendita
{
    class Program

    {
       
        MySqlConnection con = new MySqlConnection("server=localhost;User Id=root;database=tiendita");

        static void Main(string[] args)
        {
            MenuInicio();
        }

        public static void MenuInicio()
        {
            Console.WriteLine("Bienvenido a la tiendita :) ");
            Console.WriteLine("Por favor introduce un número del menú conforme a lo que quieras hacer:");
            Console.WriteLine("1) Iniciar sesión");
            Console.WriteLine("2) Registrarte");
            Console.WriteLine("0) Salir");
            string opcion = Console.ReadLine();
            switch (opcion)
            {
                case "1":
                    Login();
                    break;
                case "2":
                    Register();
                    break;
                default:
                    Console.WriteLine("Esa opción no existe, por favor introduce un numero presente en las opciones del menú");
                    break;
                case "0": return;
            }
            MenuInicio();

        }
        public static void MenuUsuario()
        {
            Console.WriteLine("Bienvenido a la tiendita");
            Console.WriteLine("Teclea 1 para buscar productos");
            Console.WriteLine("Teclea 0 para salir");
            
         
            string opcion = Console.ReadLine();
            switch (opcion)
            {
                case "1":BuscarProductos();
                    break;
                default:
                    Console.WriteLine("Esa opción no existe, por favor introduce un numero presente en las opciones del menú");
                    break;
                case "0": return;
              
            }
            MenuUsuario();

        }
        //Metodo para hacer menu de opciones
        public static void MenuAdmin()
        {
            Console.WriteLine("Menú");
            Console.WriteLine("1) Buscar producto");
            Console.WriteLine("2) Crear producto");
            Console.WriteLine("3) Actualizar producto");
            Console.WriteLine("4) Eliminar producto");
            Console.WriteLine("5) CRUD ventas");
            Console.WriteLine("6) CRUD DETALLES");
            Console.WriteLine("0) Salir");

            string opcion = Console.ReadLine();
            switch (opcion)
            {
                case "1":BuscarProductos();
                    break;
                case "2":
                    CrearProducto();
                    break;
                case "3":
                    ActualizarProducto();
                    break;
                case "4":
                    EliminarProducto();
                    break;
                case "5":
                    MenuVenta();
                    break;
                case "6":
                    MenuDetalles();
                    break;
                default:
                    Console.WriteLine("Esa opción no existe, por favor introduce un numero presente en las opciones del menú");
                    break;

                case "0": return;
            }
            MenuAdmin();
        }

        public static void MenuVenta()
        {
            Console.WriteLine("Menú");
            Console.WriteLine("1) Buscar venta");
            Console.WriteLine("2) Crear venta");
            Console.WriteLine("3) Actualizar venta");
            Console.WriteLine("4) Eliminar venta");
            Console.WriteLine("0) Regresar al menu");

            string opcion = Console.ReadLine();
            switch (opcion)
            {
                case "1":
                    BuscarVenta();
                    break;
                case "2":
                    CrearVenta();
                    break;
                case "3":
                    ActualizarVenta();
                    break;
                case "4":
                    EliminarVenta();
                    break;
                default:
                    Console.WriteLine("Esa opción no existe, por favor introduce un numero presente en las opciones del menú");
                    break;

                case "0": return;
            }
            MenuAdmin();
        }
        public static void MenuDetalles()
        {
            Console.WriteLine("Menú");
            Console.WriteLine("1) Buscar detalle");
            Console.WriteLine("2) Crear detalle");
            Console.WriteLine("3) Actualizar detalle");
            Console.WriteLine("4) Eliminar venta");
            Console.WriteLine("0) Regresar al menu");

            string opcion = Console.ReadLine();
            switch (opcion)
            {
                case "1":
                    BuscarDetalle();
                    break;
                case "2":
                    CrearDetalle();
                    break;
                case "3":
                    ActualizarDetalle();
                    break;
                case "4":
                    EliminarDetalle();
                    break;
                default:
                    Console.WriteLine("Opción invalida");
                    break;

                case "0": return;
            }
            MenuAdmin();
        }

        public static void Login()
        {
            
            Usuario usuario = new Usuario();
            Program p = new Program();
            try
            {
                Console.WriteLine("Ingresa tus datos");
                Console.Write("Nombre de Usuario: ");
                usuario.username = Console.ReadLine();
                Console.Write("Contraseña: ");
                usuario.password = Console.ReadLine();                 
                p.con.Open(); //Abrimos la conexion creada.
                MySqlCommand cmd = new MySqlCommand("SELECT username,tipo_usuario FROM usuarios WHERE username='" + usuario.username + "'AND password='" + Seguridad.Encriptar(usuario.password) + "' ", p.con); //Realizamos una selecion de la tabla usuarios.
                cmd.Parameters.AddWithValue("username", usuario.username);
                cmd.Parameters.AddWithValue("password", usuario.password);
                MySqlDataAdapter sda = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                if (dt.Rows.Count==1)
                {
                    if (dt.Rows[0][1].ToString()=="admin")
                    {
                        Console.WriteLine("Bienvenido");
                        MenuAdmin();
                    }
                    else if(dt.Rows[0][1].ToString()!="user")
                    {
                        Console.WriteLine("Hola usuario");
                        MenuUsuario();
                    }
                }
                else
                {
                    Console.WriteLine("Usuario  y/o contraseña incorrecta");
                }

            }catch(Exception e)
            {
                Console.WriteLine("Ocurrio un error" + e);
            }
            finally
            {
                p.con.Close();
            }
        }
        public static int Register()
        {
            Program p = new Program();
            Console.WriteLine("Ingresa tus datos");
            Usuario usuario = new Usuario();
            Console.Write("Usuario: ");
            usuario.username = Console.ReadLine();
            Console.Write("Contraseña: ");
            usuario.password = Seguridad.Encriptar(Console.ReadLine());
            p.con.Open();
            int result = 0;
            MySqlCommand comando = new MySqlCommand(string.Format("Insert Into usuarios (username,password,tipo_usuario) values('{0}',('{1}'),('{2}'))", usuario.username, usuario.password,"user"), p.con);
            result = comando.ExecuteNonQuery();
            p.con.Close();
            return result;
                }

        public static void BuscarDetalle()
        {
            Console.WriteLine("Buscar Detalles");
            Console.Write("Buscar: ");
            string buscar = Console.ReadLine();
          
            using (TienditaContext context = new TienditaContext())
            {
                IQueryable<Detalle> detalles = context.Detalles.Where(p => p.Id.ToString().Contains(buscar));
                foreach (Detalle detalle in detalles)
                {
                    Console.WriteLine(detalle);
                }

            }
        }

        public static void CrearDetalle()
        {
            Console.WriteLine("Crear Detalle");
            Detalle detalle = new Detalle();
            detalle = LlenarDetalle(detalle);

            using (TienditaContext context = new TienditaContext())
            {
                context.Add(detalle);
                context.SaveChanges();
                Console.WriteLine("Producto guardado satisfactoriamente");
            }
        }

        public static Detalle LlenarDetalle(Detalle detalle)
        {
            Console.WriteLine("ID de producto: ");
            detalle.ProductoId = Convert.ToUInt32(Console.ReadLine());
            Console.WriteLine("ID de la venta: ");
            detalle.VentaId = Convert.ToUInt32(Console.ReadLine());
            Console.WriteLine("Subtotal: ");
            detalle.Subtotal = decimal.Parse(Console.ReadLine());


            return detalle;


        }

        public static Detalle SeleccionarDetalle()
        {
            BuscarDetalle();
            Console.WriteLine("Seleccionar el código del Detalle");
            uint id = uint.Parse(Console.ReadLine());
            using (TienditaContext context = new TienditaContext())
            {
                Detalle detalle = context.Detalles.Find(id);
                if (detalle == null)
                {
                    SeleccionarDetalle();
                }
                return detalle;
            }
        }

        public static void ActualizarDetalle()
        {
            Console.WriteLine("Actualizar detalle");
            Detalle detalle = SeleccionarDetalle();
            detalle = LlenarDetalle(detalle);

            using (TienditaContext context = new TienditaContext())
            {
                context.Update(detalle);
                context.SaveChanges();
                Console.WriteLine("Registro Actualizado satisfactoriamente");
            }
        }

        public static void EliminarDetalle()
        {
            Console.WriteLine("Eliminar detalle");
            Detalle detalle = SeleccionarDetalle();
            using (TienditaContext context = new TienditaContext())
            {
                context.Remove(detalle);
                context.SaveChanges();
                Console.WriteLine("Registro Eliminado satisfactoriamente");
            }
        }




        public static void BuscarProductos()
        {
            Console.WriteLine("Buscar productos");
            Console.WriteLine("Buscar: ");
            string buscar = Console.ReadLine();
            using(TienditaContext context = new TienditaContext())
            {
                
                IQueryable<Producto> productos = context.Productos.Where(p => p.Nombre.Contains(buscar));
             foreach (Producto producto in productos)
                {
                    Console.WriteLine(producto);
                }
            }
        }
        public static void Productos()
        {
            Console.WriteLine("Buscar productos");
            Console.WriteLine("Buscar: ");
            string buscar = Console.ReadLine();
            using (TienditaContext context = new TienditaContext())
            {

                IQueryable<Producto> productos = context.Productos.Where(p => p.Nombre.Contains(buscar));
                foreach (Producto producto in productos)
                {
                    Console.WriteLine(producto);
                }
            }
        }
        public static void CrearProducto()
        {
            Console.WriteLine("Crear producto");
            Producto producto = new Producto();
            producto = LlenarProducto(producto);

            using (TienditaContext context = new TienditaContext())
            {
                context.Add(producto);
                context.SaveChanges();
                Console.WriteLine("Producto creado");
            }
        }

        public static Producto LlenarProducto(Producto producto)
        {
            Console.Write("Nombre: ");
            producto.Nombre = Console.ReadLine();
            Console.Write("Descripción: ");
            producto.Descripcion = Console.ReadLine();
            Console.Write("Precio: ");
            producto.Precio = decimal.Parse(Console.ReadLine());
            Console.Write("Costo: ");
            producto.Costo = decimal.Parse(Console.ReadLine());
            Console.Write("Cantidad: ");
            producto.Cantidad = decimal.Parse(Console.ReadLine());
            Console.Write("Tamaño: ");
            producto.Tamano = Console.ReadLine();

            return producto;
        }

        public static Producto SelecionarProducto()
        {
            BuscarProductos();
            Console.Write("Seleciona el código de producto: ");
            uint id = uint.Parse(Console.ReadLine());
            using (TienditaContext context = new TienditaContext())
            {
                Producto producto = context.Productos.Find(id);
                if (producto == null)
                {
                    SelecionarProducto();
                }
                return producto;
            }
        }

        public static void ActualizarProducto()
        {
            Console.WriteLine("Actualizar producto");
            Producto producto = SelecionarProducto();
            producto = LlenarProducto(producto);
            using (TienditaContext context = new TienditaContext())
            {
                context.Update(producto);
                context.SaveChanges();
                Console.WriteLine("Producto actualizado");
            }
        }

        public static void EliminarProducto()
        {
            Console.WriteLine("Eliminar producto");
            Producto producto = SelecionarProducto();
            using (TienditaContext context = new TienditaContext())
            {
                context.Remove(producto);
                context.SaveChanges();
                Console.WriteLine("Producto eliminado");
            }
        }

        public static void BuscarVenta()
        {
            Console.WriteLine("Buscar ventas");
            Console.WriteLine("Buscar: ");
            string buscar = Console.ReadLine();
            using (TienditaContext context = new TienditaContext())
            {

                IQueryable<Venta> ventas = context.Ventas.Where(p => p.Cliente.Contains(buscar));
                foreach (Venta venta in ventas)
                {
                    Console.WriteLine(venta);
                }
            }
        }
        public static void CrearVenta()
    {
        Console.WriteLine("Crear detalle");
            Venta venta = new Venta();
            venta = LlenarVenta (venta);

        using (TienditaContext context = new TienditaContext())
        {
            context.Add(venta);
            context.SaveChanges();
            Console.WriteLine("venta creado");
        }
    }

    public static Venta LlenarVenta(Venta venta)
    {
        Console.Write("total: ");
            venta.Total = decimal.Parse(Console.ReadLine());

            DateTime fechaHoy = DateTime.Today;
            venta.fecha = fechaHoy;
            Console.Write("fecha: "+fechaHoy);
            Console.Write("Cliente: ");
            venta.Cliente = Console.ReadLine();


            return venta;
    }
        public static Venta SelecionarVenta()
        {
            BuscarProductos();
            Console.Write("Seleciona el código de venta: ");
            uint id = uint.Parse(Console.ReadLine());
            using (TienditaContext context = new TienditaContext())
            {
                Venta venta = context.Ventas.Find(id);
                if (venta == null)
                {
                    SelecionarVenta();
                }
                return venta;
            }
        }
        public static void ActualizarVenta()
        {
            Console.WriteLine("Actualizar venta");
            Venta venta = SelecionarVenta();
            venta = LlenarVenta(venta);
            using (TienditaContext context = new TienditaContext())
            {
                context.Update(venta);
                context.SaveChanges();
                Console.WriteLine("venta actualizada");
            }
        }

        public static void EliminarVenta()
        {
            Console.WriteLine("Eliminar venta");
            Venta venta = SelecionarVenta();
            using (TienditaContext context = new TienditaContext())
            {
                context.Remove(venta);
                context.SaveChanges();
                Console.WriteLine("venta eliminada");
            }
        }
    }
    public static class Seguridad
        {
            /// Encripta una cadena
            public static string Encriptar(this string _cadenaAencriptar)
        {
            string result = string.Empty;
            byte[] encryted = System.Text.Encoding.Unicode.GetBytes(_cadenaAencriptar);
            result = Convert.ToBase64String(encryted);
            return result;
        }

        /// Esta función desencripta la cadena que le envíamos en el parámentro de entrada.
        public static string DesEncriptar(this string _cadenaAdesencriptar)
        {
            string result = string.Empty;
            byte[] decryted = Convert.FromBase64String(_cadenaAdesencriptar);
            //result = System.Text.Encoding.Unicode.GetString(decryted, 0, decryted.ToArray().Length);
            result = System.Text.Encoding.Unicode.GetString(decryted);
            return result;
        }



    }
}

