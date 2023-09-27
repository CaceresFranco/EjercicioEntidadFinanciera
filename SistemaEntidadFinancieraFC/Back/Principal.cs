namespace Back
{
    public class Principal
    {
        // inicialice aca arriba al db context porque si tiraba solo la linea del principal pelada como en la ppt, me tiraba error (habre hecho algo mal?)
        public BancoDbContext dbContext;

        public Principal()
        {
            BancoDbContext dbContext = new BancoDbContext();
        }

        public void AgregarCliente(Cliente cliente)
        {
            dbContext.Clientes.Add(cliente);
            dbContext.SaveChanges();
        }

        public void CrearCuentaBancaria(CuentaBancaria cuenta)
        {
            dbContext.CuentasBancarias.Add(cuenta);
            dbContext.SaveChanges();
        }

        public void EmitirTarjetaCredito(TarjetaCredito tarjeta)
        {
            dbContext.TarjetasCredito.Add(tarjeta);
            dbContext.SaveChanges();
        }

        public void RealizarDeposito(int cuentaId, decimal monto)
        {
            var cuenta = dbContext.CuentasBancarias.Find(cuentaId);
            if (cuenta != null)
            {
                cuenta.Saldo += monto;
                dbContext.SaveChanges();
            }
        }

        public string RealizarExtraccion(int cuentaId, decimal monto)
        {
            var cuenta = dbContext.CuentasBancarias.Find(cuentaId);
            if (cuenta != null)
            {
                if (monto <= cuenta.Saldo)
                {
                    cuenta.Saldo -= monto;
                    dbContext.SaveChanges();
                    return "Extracción exitosa";
                }
                else
                {
                    return "Saldo insuficiente";
                }
            }
            else
            {
                return "Cuenta no encontrada";
            }
        }

        public string RealizarTransferencia(int cuentaOrigenId, int cuentaDestinoId, decimal monto)
        {
            var cuentaOrigen = dbContext.CuentasBancarias.Find(cuentaOrigenId);
            var cuentaDestino = dbContext.CuentasBancarias.Find(cuentaDestinoId);

            if (cuentaOrigen != null && cuentaDestino != null)
            {
                if (monto <= cuentaOrigen.Saldo)
                {
                    cuentaOrigen.Saldo -= monto;
                    cuentaDestino.Saldo += monto;
                    dbContext.SaveChanges();
                    return "Transferencia exitosa";
                }
                else
                {
                    return "Saldo insuficiente en la cuenta de origen";
                }
            }
            else
            {
                return "Cuenta de origen o destino no encontrada";
            }
        }

        public string PagarTarjetaCredito(int tarjetaId, decimal montoPago)
        {
            var tarjeta = dbContext.TarjetasCredito.Find(tarjetaId);
            if (tarjeta != null)
            {
                if (montoPago <= tarjeta.MontoDeuda)
                {
                    tarjeta.MontoDeuda -= montoPago;
                    dbContext.SaveChanges();
                    return "Pago realizado con éxito";
                }
                else
                {
                    return "El monto del pago supera la deuda actual";
                }
            }
            return "Tarjeta de crédito no encontrada";
        }

        public string GenerarResumenTarjeta(int tarjetaId)
        {
            var tarjeta = dbContext.TarjetasCredito.Find(tarjetaId);
            if (tarjeta != null)
            {
                // resumen solo muestra saldo disponible y la deuda, descubri en internet que agregando el ":c2)" al final de la linea devuelve el numero con solo 2 decimales
                string resumen = $"Resumen de Tarjeta de Crédito\n";
                resumen += $"Saldo Disponible: {tarjeta.SaldoDisponible:C2}\n";
                resumen += $"Monto Deuda: {tarjeta.MontoDeuda:C2}\n";

                return resumen;
            }
            return "Tarjeta de crédito no encontrada";

            // no me termina mucho de convencer el resumen, pero realmente no se me ocurria como hacerlo y robe la idea de internet ( tambien aprendi un par de cosas como el &&)
            //despues voy a tener que trabajar todos estoss strings que hice en los returns con unos messagebox.Show
        }
    }
}