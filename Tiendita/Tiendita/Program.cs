﻿using MySql.Data.MySqlClient;
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
            Console.WriteLine("Bienvenido a la tiendita uwu: ");
            Console.WriteLine("¿Que quieres hacer?");
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
                    Console.WriteLine("Opción invalida");
                    break;
                case "0": return;
            }
            MenuInicio();

        }
        public static void MenuUsuario()
        {
            Console.WriteLine("Bienvenido a la tiendita uwu ");
            Console.WriteLine("Teclea 1 para buscar productos");
            Console.WriteLine("Teclea 0 para salir");
            
         
            string opcion = Console.ReadLine();
            switch (opcion)
            {
                case "1":BuscarProductos();
                    break;
                default:
                    Console.WriteLine("Opción invalida");
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
                Console.Write("Usuario: ");
                usuario.username = Console.ReadLine();
                Console.Write("Contraseña: ");
                usuario.password = Console.ReadLine();
                p.con.Open(); //Abrimos la conexion creada.
                MySqlCommand cmd = new MySqlCommand("SELECT username,tipo_usuario FROM usuarios WHERE username='" + usuario.username + "'AND password='" + usuario.password + "' ", p.con); //Realizamos una selecion de la tabla usuarios.
                cmd.Parameters.AddWithValue("username", usuario.username);
                cmd.Parameters.AddWithValue("password", usuario.password);
                MySqlDataAdapter sda = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                if (dt.Rows.Count==1)
                {
                    if (dt.Rows[0][1].ToString()=="admin")
                    {
                        Console.WriteLine("Hola vato");
                        MenuAdmin();
                    }
                    else if(dt.Rows[0][1].ToString()!="admin")
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
            usuario.password = Console.ReadLine();
            p.con.Open();
            int result = 0;
            MySqlCommand comando = new MySqlCommand(string.Format("Insert Into usuarios (username,password,tipo_usuario) values('{0}',sha('{1}'),('{2}'))", usuario.username, usuario.password,"admin"), p.con);
            result = comando.ExecuteNonQuery();
            p.con.Close();
            return result;
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
    }

}

