# ✅ Checklist de Pruebas de Aceptación – Axenta

## 1. Períodos Contables
- [ ] Crear período → validar fechas correctas, estado inicial = abierto.
- [ ] Cerrar período → no deben permitirse nuevos asientos.
- [ ] Reabrir período → generar eventos de reversión y actualizar auditoría.
- [ ] No se puede reabrir un período ya abierto.

## 2. Asientos Contables (Journal Entries)
- [ ] Crear asiento con líneas balanceadas (Debe = Haber).
- [ ] Crear asiento en período cerrado → debe fallar.
- [ ] Actualizar asiento existente en período abierto.
- [ ] Reversar asiento → validar creación del asiento opuesto.
- [ ] Asociar documentos y verificarlos en consulta.

## 3. Auditoría y Eventos
- [ ] Evento `JournalEntryCreatedEvent` crea registro en `AuditLogs`.
- [ ] Evento de integración en RabbitMQ publicado correctamente.
- [ ] `EventLog` registra y evita duplicados (idempotencia).
- [ ] Logs de operaciones visibles en Graylog/Serilog.

## 4. Catálogos y Configuración
- [ ] CRUD de cuentas contables.
- [ ] Activar/Desactivar cuenta.
- [ ] CRUD de compañías.
- [ ] CRUD de tipos de cambio y obtener tipo diario.
- [ ] CRUD de centros de costo (nueva fase).
- [ ] Validar que `JournalEntryLine` puede asociar centro de costo.

## 5. Reportes Contables
- [ ] Estado de resultados por período y compañía.
- [ ] Balance general.
- [ ] Balance de comprobación.
- [ ] Libro mayor.
- [ ] Exportar a PDF y Excel (fase 2).
- [ ] Reportes filtrados por centro de costo (fase 2).

## 6. Seguridad y Usuarios
- [ ] Validar identidad y `performedBy` en auditoría.
- [ ] Autenticación JWT (pendiente para fase posterior).
- [ ] Rate limiting (opcional).

## 7. Infraestructura
- [ ] Configuración de credenciales dinámicas con Vault.
- [ ] Telemetría básica expuesta y métricas visibles en Prometheus/Grafana.
- [ ] API Gateway con YARP funcionando.  
