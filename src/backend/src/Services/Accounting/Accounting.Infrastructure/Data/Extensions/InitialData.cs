namespace Accounting.Infrastructure.Data.Extensions;

public class InitialData
{
    public static IEnumerable<AccountType> AccountTypes =>
        new List<AccountType>
        {
            AccountType.Create(AccountTypeId.Of(new Guid("9546c264-0869-44d9-8031-371159b76d3f")), "Activo",
                "Cuentas de activo"),
            AccountType.Create(AccountTypeId.Of(new Guid("d508e0ce-20d8-4e91-9e62-7a5d122aad2d")), "Pasivo",
                "Cuentas de pasivo"),
            AccountType.Create(AccountTypeId.Of(new Guid("77084a67-ab6c-4a75-b673-2094ad375c0b")), "Patrimonio",
                "Cuentas de capital"),
            AccountType.Create(AccountTypeId.Of(new Guid("6e705d5f-2d5c-449e-a187-17dc00d2c914")), "Ingreso",
                "Cuentas de ingresos"),
            AccountType.Create(AccountTypeId.Of(new Guid("71bbd6e0-abf4-4f2a-afec-9199bb404b08")), "Gasto",
                "Cuentas de gastos")
        };

    public static IEnumerable<Account> Accounts =>
        new List<Account>
        {
            // Active accounts
            Account.Create(
                AccountId.Of(new Guid("7573e77c-110b-4a3b-9bda-7be306bb14b4")),
                "1",
                "Activo",
                AccountTypeId.Of(new Guid("9546c264-0869-44d9-8031-371159b76d3f")),
                null,
                1,
                false),

            Account.Create(
                AccountId.Of(new Guid("cacfc56f-82ca-4cd5-b694-540b5e1b2e03")),
                "1.1",
                "Activo Corriente",
                AccountTypeId.Of(new Guid("9546c264-0869-44d9-8031-371159b76d3f")),
                AccountId.Of(new Guid("7573e77c-110b-4a3b-9bda-7be306bb14b4")),
                2,
                false),

            Account.Create(
                AccountId.Of(new Guid("b603261e-272e-4aa6-9398-0dfb1a617d89")),
                "1.1.1",
                "Efectivo y Equivalentes",
                AccountTypeId.Of(new Guid("9546c264-0869-44d9-8031-371159b76d3f")),
                AccountId.Of(new Guid("cacfc56f-82ca-4cd5-b694-540b5e1b2e03")),
                3,
                false),

            Account.Create(
                AccountId.Of(new Guid("23b79d9f-96d3-4c20-ac8e-4387a70152b3")),
                "1.1.1.01",
                "Caja",
                AccountTypeId.Of(new Guid("9546c264-0869-44d9-8031-371159b76d3f")),
                AccountId.Of(new Guid("b603261e-272e-4aa6-9398-0dfb1a617d89")),
                4,
                true),

            Account.Create(
                AccountId.Of(new Guid("a06a0526-6d78-487d-bca8-d4b4cb57ac00")),
                "1.1.1.02",
                "Bancos",
                AccountTypeId.Of(new Guid("9546c264-0869-44d9-8031-371159b76d3f")),
                AccountId.Of(new Guid("b603261e-272e-4aa6-9398-0dfb1a617d89")),
                4,
                true),

            Account.Create(
                AccountId.Of(new Guid("c1d8a1d0-6ae0-4cf1-b7e5-a5e1a799e7ff")),
                "1.1.2",
                "Cuentas por Cobrar",
                AccountTypeId.Of(new Guid("9546c264-0869-44d9-8031-371159b76d3f")),
                AccountId.Of(new Guid("cacfc56f-82ca-4cd5-b694-540b5e1b2e03")),
                3,
                false),

            Account.Create(
                AccountId.Of(new Guid("797cc7bd-99f4-4f77-a8d6-9a0e30c152bf")),
                "1.1.2.01",
                "Clientes",
                AccountTypeId.Of(new Guid("9546c264-0869-44d9-8031-371159b76d3f")),
                AccountId.Of(new Guid("c1d8a1d0-6ae0-4cf1-b7e5-a5e1a799e7ff")),
                4,
                true),

            Account.Create(
                AccountId.Of(new Guid("2b762087-0386-416f-afbc-5a9b0450066b")),
                "1.2",
                "Activo NO Corrientes",
                AccountTypeId.Of(new Guid("9546c264-0869-44d9-8031-371159b76d3f")),
                AccountId.Of(new Guid("7573e77c-110b-4a3b-9bda-7be306bb14b4")),
                2,
                false),

            Account.Create(
                AccountId.Of(new Guid("dc706352-fb79-41b4-8736-f4a0aa68c807")),
                "1.2.1",
                "Propiedades y Vehiculos",
                AccountTypeId.Of(new Guid("9546c264-0869-44d9-8031-371159b76d3f")),
                AccountId.Of(new Guid("2b762087-0386-416f-afbc-5a9b0450066b")),
                3,
                false),

            Account.Create(
                AccountId.Of(new Guid("66eee587-bee4-4cac-af59-b8e8a0b77206")),
                "1.2.1.1",
                "Terrenos",
                AccountTypeId.Of(new Guid("9546c264-0869-44d9-8031-371159b76d3f")),
                AccountId.Of(new Guid("dc706352-fb79-41b4-8736-f4a0aa68c807")),
                4,
                true),

            Account.Create(
                AccountId.Of(new Guid("7446ddcb-7be6-46e4-94de-1b5275cd8b0f")),
                "1.2.1.2",
                "Edificios",
                AccountTypeId.Of(new Guid("9546c264-0869-44d9-8031-371159b76d3f")),
                AccountId.Of(new Guid("dc706352-fb79-41b4-8736-f4a0aa68c807")),
                4,
                true),

            Account.Create(
                AccountId.Of(new Guid("4d9adb9e-ad24-4e0e-803a-7b25e953e560")),
                "1.2.1.3",
                "Vehiculos",
                AccountTypeId.Of(new Guid("9546c264-0869-44d9-8031-371159b76d3f")),
                AccountId.Of(new Guid("dc706352-fb79-41b4-8736-f4a0aa68c807")),
                4,
                true),

            Account.Create(
                AccountId.Of(new Guid("0e37ad39-7742-48d5-8b5a-20df37e10f7e")),
                "1.1.3",
                "Inventario",
                AccountTypeId.Of(new Guid("9546c264-0869-44d9-8031-371159b76d3f")),
                AccountId.Of(new Guid("cacfc56f-82ca-4cd5-b694-540b5e1b2e03")),
                3,
                false),

            Account.Create(
                AccountId.Of(new Guid("057a93fb-c93b-4cf7-a79e-fab3583f2f13")),
                "1.1.3.1",
                "Inventario a la venta",
                AccountTypeId.Of(new Guid("9546c264-0869-44d9-8031-371159b76d3f")),
                AccountId.Of(new Guid("cacfc56f-82ca-4cd5-b694-540b5e1b2e03")),
                4,
                true),

            Account.Create(
                AccountId.Of(new Guid("d29ece51-323d-4c36-9193-9bbf3b04b962")),
                "1.1.3.2",
                "Suministros",
                AccountTypeId.Of(new Guid("9546c264-0869-44d9-8031-371159b76d3f")),
                AccountId.Of(new Guid("cacfc56f-82ca-4cd5-b694-540b5e1b2e03")),
                4,
                true),

            Account.Create(
                AccountId.Of(new Guid("4ddb9bba-f3c2-4f01-90ae-3e0f599857e6")),
                "2",
                "Pasivo",
                AccountTypeId.Of(new Guid("d508e0ce-20d8-4e91-9e62-7a5d122aad2d")),
                null,
                1,
                false),

            Account.Create(
                AccountId.Of(new Guid("0b240be5-2262-4f7f-949e-8798576f898b")),
                "2.1",
                "Pasivo  Corriente",
                AccountTypeId.Of(new Guid("d508e0ce-20d8-4e91-9e62-7a5d122aad2d")),
                AccountId.Of(new Guid("4ddb9bba-f3c2-4f01-90ae-3e0f599857e6")),
                2,
                false),

            Account.Create(
                AccountId.Of(new Guid("a04ee151-03d4-4840-8697-600360d6977d")),
                "2.1.1",
                "Proveedores",
                AccountTypeId.Of(new Guid("d508e0ce-20d8-4e91-9e62-7a5d122aad2d")),
                AccountId.Of(new Guid("0b240be5-2262-4f7f-949e-8798576f898b")),
                3,
                true),

            Account.Create(
                AccountId.Of(new Guid("eb95f0b8-05fb-4d49-b561-e38cabccfdca")),
                "2.1.2",
                "Impuestos por Pagar",
                AccountTypeId.Of(new Guid("d508e0ce-20d8-4e91-9e62-7a5d122aad2d")),
                AccountId.Of(new Guid("0b240be5-2262-4f7f-949e-8798576f898b")),
                3,
                true),

            Account.Create(
                AccountId.Of(new Guid("be842568-cda5-4b67-94e0-b23d4b8c551d")),
                "2.2",
                "Pasivo NO Corriente",
                AccountTypeId.Of(new Guid("d508e0ce-20d8-4e91-9e62-7a5d122aad2d")),
                AccountId.Of(new Guid("4ddb9bba-f3c2-4f01-90ae-3e0f599857e6")),
                2,
                false),

            Account.Create(
                AccountId.Of(new Guid("f4c0eb3c-eefa-49e2-b72b-01b55105d7b7")),
                "2.2.1",
                "Documento por pagar",
                AccountTypeId.Of(new Guid("d508e0ce-20d8-4e91-9e62-7a5d122aad2d")),
                AccountId.Of(new Guid("be842568-cda5-4b67-94e0-b23d4b8c551d")),
                3,
                true),

            // Equity accounts
            Account.Create(
                AccountId.Of(new Guid("b5f50c2b-6625-4372-8e45-e6df172d1775")),
                "3",
                "Patrimonio",
                AccountTypeId.Of(new Guid("77084a67-ab6c-4a75-b673-2094ad375c0b")),
                null,
                1,
                false),

            Account.Create(
                AccountId.Of(new Guid("a91a4d34-c88e-4f29-8e31-03986820bada")),
                "3.1",
                "Capital Social",
                AccountTypeId.Of(new Guid("77084a67-ab6c-4a75-b673-2094ad375c0b")),
                AccountId.Of(new Guid("b5f50c2b-6625-4372-8e45-e6df172d1775")),
                2,
                true),

            Account.Create(
                AccountId.Of(new Guid("9112f5ea-1bc4-47a3-a973-27d52b0ee945")),
                "3.2",
                "Utilidades Acumuladas",
                AccountTypeId.Of(new Guid("77084a67-ab6c-4a75-b673-2094ad375c0b")),
                AccountId.Of(new Guid("b5f50c2b-6625-4372-8e45-e6df172d1775")),
                2,
                true),

            // Income accounts
            Account.Create(
                AccountId.Of(new Guid("55c126a9-3240-463b-95c3-80dd9dc7e593")),
                "4",
                "Ingresos",
                AccountTypeId.Of(new Guid("6e705d5f-2d5c-449e-a187-17dc00d2c914")),
                null,
                1,
                false),

            Account.Create(
                AccountId.Of(new Guid("b38efb88-a90d-4d10-b1fa-8e23dd7ae5ce")),
                "4.1",
                "Ventas",
                AccountTypeId.Of(new Guid("6e705d5f-2d5c-449e-a187-17dc00d2c914")),
                AccountId.Of(new Guid("55c126a9-3240-463b-95c3-80dd9dc7e593")),
                2,
                false),

            Account.Create(
                AccountId.Of(new Guid("b33d3d96-4b34-44ae-9fe5-2fc8d657b115")),
                "4.1.1",
                "Ventas Nacionales",
                AccountTypeId.Of(new Guid("6e705d5f-2d5c-449e-a187-17dc00d2c914")),
                AccountId.Of(new Guid("b38efb88-a90d-4d10-b1fa-8e23dd7ae5ce")),
                3,
                true),

            Account.Create(
                AccountId.Of(new Guid("573c0ea6-eb18-4c15-8824-8c040892b8f4")),
                "4.1.2",
                "Ventas al Exterior",
                AccountTypeId.Of(new Guid("6e705d5f-2d5c-449e-a187-17dc00d2c914")),
                AccountId.Of(new Guid("b38efb88-a90d-4d10-b1fa-8e23dd7ae5ce")),
                3,
                true),

            // Outflow accounts
            Account.Create(
                AccountId.Of(new Guid("0bd459fa-3d72-4bfa-ab52-637b50f288ad")),
                "5",
                "Egresos",
                AccountTypeId.Of(new Guid("71bbd6e0-abf4-4f2a-afec-9199bb404b08")),
                null,
                1,
                false),

            Account.Create(
                AccountId.Of(new Guid("da40d7ca-c24c-4502-a443-80859407c725")),
                "5.1",
                "Costos",
                AccountTypeId.Of(new Guid("71bbd6e0-abf4-4f2a-afec-9199bb404b08")),
                AccountId.Of(new Guid("0bd459fa-3d72-4bfa-ab52-637b50f288ad")),
                2,
                false),

            Account.Create(
                AccountId.Of(new Guid("05607817-50d5-4261-af7e-d1388e7c5405")),
                "5.1.1",
                "Costo de Mercaderías Vendidas",
                AccountTypeId.Of(new Guid("71bbd6e0-abf4-4f2a-afec-9199bb404b08")),
                AccountId.Of(new Guid("da40d7ca-c24c-4502-a443-80859407c725")),
                3,
                false),

            Account.Create(
                AccountId.Of(new Guid("15165f65-a55a-4f7c-8d87-9275301217af")),
                "5.2",
                "Gastos Operativos",
                AccountTypeId.Of(new Guid("71bbd6e0-abf4-4f2a-afec-9199bb404b08")),
                AccountId.Of(new Guid("0bd459fa-3d72-4bfa-ab52-637b50f288ad")),
                2,
                false),

            Account.Create(
                AccountId.Of(new Guid("e769d46b-268d-463c-8074-71983ead323f")),
                "5.2.1",
                "Sueldos y Salarios",
                AccountTypeId.Of(new Guid("71bbd6e0-abf4-4f2a-afec-9199bb404b08")),
                AccountId.Of(new Guid("15165f65-a55a-4f7c-8d87-9275301217af")),
                3,
                false),

            Account.Create(
                AccountId.Of(new Guid("e19f4576-6224-40e3-8b03-01343b413df7")),
                "5.2.2",
                "Servicios Públicos",
                AccountTypeId.Of(new Guid("71bbd6e0-abf4-4f2a-afec-9199bb404b08")),
                AccountId.Of(new Guid("15165f65-a55a-4f7c-8d87-9275301217af")),
                3,
                false),
        };


    public static IEnumerable<Period> Periods =>
        new List<Period>
        {
            Period.Create(PeriodId.Of(new Guid("e44ed594-272c-4978-a3b5-11fb47e9ca12")),
                2014,
                10
            )
        };

    public static IEnumerable<JournalEntry> JournalEntriesWithLines
    {
        get
        {
            /*
             * journal Entry 1
             */
            var journalEntry1 = JournalEntry.Create(
                JournalEntryId.Of(new Guid("b45e75af-fe29-4c2e-9653-aad9bf5b42ef")),
                DateTime.SpecifyKind(new DateTime(2014, 10, 01), DateTimeKind.Utc),
                "Registro del aporte",
                PeriodId.Of(new Guid("e44ed594-272c-4978-a3b5-11fb47e9ca12"))
            );

            journalEntry1.AddLine(
                AccountId.Of(new Guid("b603261e-272e-4aa6-9398-0dfb1a617d89")),
                5000000,
                0
            );

            journalEntry1.AddLine(
                AccountId.Of(new Guid("a91a4d34-c88e-4f29-8e31-03986820bada")),
                0,
                5000000,
                2
            );


            /*
             * journal Entry 2
             */
            var journalEntry2 = JournalEntry.Create(
                JournalEntryId.Of(new Guid("0b55189d-ce04-471f-abbb-f73208be063a")),
                DateTime.SpecifyKind(new DateTime(2014, 10, 15), DateTimeKind.Utc),
                "Registro de préstamo con la entidad financiera",
                PeriodId.Of(new Guid("e44ed594-272c-4978-a3b5-11fb47e9ca12"))
            );

            journalEntry2.AddLine(
                AccountId.Of(new Guid("66eee587-bee4-4cac-af59-b8e8a0b77206")),
                15000000,
                0
            );

            journalEntry2.AddLine(
                AccountId.Of(new Guid("7446ddcb-7be6-46e4-94de-1b5275cd8b0f")),
                15000000,
                0
            );

            journalEntry2.AddLine(
                AccountId.Of(new Guid("f4c0eb3c-eefa-49e2-b72b-01b55105d7b7")),
                50000000,
                0
            );

            /*
             * journal Entry 3
             */

            var journalEntry3 = JournalEntry.Create(
                JournalEntryId.Of(new Guid("56cb166e-07b6-44f7-bd8b-cbc16c595946")),
                DateTime.SpecifyKind(new DateTime(2014, 10, 20), DateTimeKind.Utc),
                "Compra de mercadería para la venta",
                PeriodId.Of(new Guid("e44ed594-272c-4978-a3b5-11fb47e9ca12"))
            );

            journalEntry3.AddLine(
                AccountId.Of(new Guid("057a93fb-c93b-4cf7-a79e-fab3583f2f13")),
                2000000,
                0
            );

            journalEntry3.AddLine(
                AccountId.Of(new Guid("a04ee151-03d4-4840-8697-600360d6977d")),
                0,
                2000000
            );

            /*
             * journal Entry 4
             */

            var journalEntry4 = JournalEntry.Create(
                JournalEntryId.Of(new Guid("a265189a-f34a-4889-85c6-3ee500d3549b")),
                DateTime.SpecifyKind(new DateTime(2014, 10, 22), DateTimeKind.Utc),
                "Compra de motocicleta para mensajero",
                PeriodId.Of(new Guid("e44ed594-272c-4978-a3b5-11fb47e9ca12"))
            );

            journalEntry4.AddLine(
                AccountId.Of(new Guid("4d9adb9e-ad24-4e0e-803a-7b25e953e560")),
                2500000,
                0
            );

            journalEntry4.AddLine(
                AccountId.Of(new Guid("a06a0526-6d78-487d-bca8-d4b4cb57ac00")),
                0,
                2500000
            );

            /*
             * journal Entry 5
             */

            var journalEntry5 = JournalEntry.Create(
                JournalEntryId.Of(new Guid("aacd74d4-cde3-4f54-ab34-d3843091788f")),
                DateTime.SpecifyKind(new DateTime(2014, 10, 22), DateTimeKind.Utc),
                "Abono a cuenta por pagar\"",
                PeriodId.Of(new Guid("e44ed594-272c-4978-a3b5-11fb47e9ca12"))
            );

            journalEntry5.AddLine(
                AccountId.Of(new Guid("a04ee151-03d4-4840-8697-600360d6977d")),
                1000000,
                0
            );

            journalEntry5.AddLine(
                AccountId.Of(new Guid("23b79d9f-96d3-4c20-ac8e-4387a70152b3")),
                0,
                1000000
            );

            /*
             * journal Entry 6
             */

            var journalEntry6 = JournalEntry.Create(
                JournalEntryId.Of(new Guid("a2386d1c-c53e-4f12-ad38-f2ea5a6c4081")),
                DateTime.SpecifyKind(new DateTime(2014, 10, 30), DateTimeKind.Utc),
                "Gasto por salarios",
                PeriodId.Of(new Guid("e44ed594-272c-4978-a3b5-11fb47e9ca12"))
            );

            journalEntry6.AddLine(
                AccountId.Of(new Guid("e769d46b-268d-463c-8074-71983ead323f")),
                800000,
                0
            );

            journalEntry6.AddLine(
                AccountId.Of(new Guid("e19f4576-6224-40e3-8b03-01343b413df7")),
                500000,
                0);

            journalEntry6.AddLine(
                AccountId.Of(new Guid("a06a0526-6d78-487d-bca8-d4b4cb57ac00")),
                0,
                1300000);

            return new List<JournalEntry>
                { journalEntry1, journalEntry2, journalEntry3, journalEntry4, journalEntry5, journalEntry6 };
        }
    }

    public static IEnumerable<DocumentReference> DocumentReferencesRelatedToJournalEntry =>
        new List<DocumentReference>
        {
            // Active accounts
            DocumentReference.Create(
                JournalEntryId.Of(new Guid("0b55189d-ce04-471f-abbb-f73208be063a")),
                "Loan",
                SourceId.Of(new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6")),
                "123456789", "Registro de préstamo con la entidad financiera"
            )
        };
}