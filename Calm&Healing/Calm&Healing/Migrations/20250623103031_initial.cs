using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Calm_Healing.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "address",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    uuid = table.Column<Guid>(type: "uuid", nullable: false),
                    line1 = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    line2 = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    city = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    state = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    zipcode = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    created_by = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    modified_by = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    created = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    modified = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("address_pkey", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "client_clinician",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    client_id = table.Column<Guid>(type: "uuid", nullable: false),
                    clinician_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_by = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    modified_by = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    modified = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("client_clinician_pkey", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "databasechangelog",
                columns: table => new
                {
                    id = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    author = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    filename = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    dateexecuted = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    orderexecuted = table.Column<int>(type: "integer", nullable: false),
                    exectype = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    md5sum = table.Column<string>(type: "character varying(35)", maxLength: 35, nullable: true),
                    description = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    comments = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    tag = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    liquibase = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    contexts = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    labels = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    deployment_id = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "databasechangeloglock",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false),
                    locked = table.Column<bool>(type: "boolean", nullable: false),
                    lockgranted = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    lockedby = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("databasechangeloglock_pkey", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "emergency_contact",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    uuid = table.Column<Guid>(type: "uuid", nullable: true),
                    name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    phone_number = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    relationship = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    responsible_party = table.Column<bool>(type: "boolean", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("emergency_contact_pkey", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "fee_schedule",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    uuid = table.Column<Guid>(type: "uuid", nullable: true),
                    procedure_code = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    code_type = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    rate = table.Column<double>(type: "double precision", nullable: true),
                    created_by = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    modified_by = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    created = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "now()"),
                    modified = table.Column<DateTime>(type: "timestamp without time zone", nullable: true, defaultValueSql: "now()"),
                    archive = table.Column<bool>(type: "boolean", nullable: true, defaultValue: false),
                    status = table.Column<bool>(type: "boolean", nullable: true, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("fee_schedule_pkey", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "forms",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    uuid = table.Column<Guid>(type: "uuid", nullable: false),
                    form_title = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    form_type = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    form_key = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    status = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    archive = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    auto_assign = table.Column<bool>(type: "boolean", nullable: false),
                    form_status = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    created_by = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    modified_by = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    created = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "now()"),
                    modified = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("forms_pkey", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "notifications",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    uuid = table.Column<Guid>(type: "uuid", nullable: true),
                    title = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    description = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    notification_type = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    marked_as_read = table.Column<bool>(type: "boolean", nullable: true),
                    data = table.Column<string>(type: "text", nullable: true),
                    user_id = table.Column<Guid>(type: "uuid", nullable: true),
                    created_by = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    modified_by = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    created = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    modified = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("notifications_pkey", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "permissions",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    uuid = table.Column<Guid>(type: "uuid", nullable: true),
                    permission = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    permission_key = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    created_by = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    modified_by = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    created = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "now()"),
                    modified = table.Column<DateTime>(type: "timestamp without time zone", nullable: true, defaultValueSql: "now()"),
                    group = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("permissions_pkey", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "roles",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    uuid = table.Column<Guid>(type: "uuid", nullable: true),
                    role_name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    created_by = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    modified_by = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    created = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "now()"),
                    modified = table.Column<DateTime>(type: "timestamp without time zone", nullable: true, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("roles_pkey", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "sticky_note",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    uuid = table.Column<Guid>(type: "uuid", nullable: false),
                    note = table.Column<string>(type: "text", nullable: true),
                    client_id = table.Column<Guid>(type: "uuid", nullable: true),
                    created_by = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    modified_by = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    created = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    modified = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    alert_note = table.Column<string>(type: "text", nullable: true),
                    alert_note_created_by = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    sticky_note_created_by = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    alert_note_modified = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    sticky_note_modified = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("sticky_note_pkey", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    uuid = table.Column<Guid>(type: "uuid", nullable: false),
                    iam_id = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    email = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    first_name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    last_name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    last_login = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    active = table.Column<bool>(type: "boolean", nullable: false),
                    archive = table.Column<bool>(type: "boolean", nullable: false),
                    email_verified = table.Column<bool>(type: "boolean", nullable: false),
                    phone_verified = table.Column<bool>(type: "boolean", nullable: false),
                    created = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    modified = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    created_by = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    modified_by = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    middle_name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    phone_number = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("users_pkey", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "contacts",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    uuid = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    contact_number = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    email_id = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    fax_number = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    status = table.Column<bool>(type: "boolean", nullable: true, defaultValue: true),
                    contact_type = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    archive = table.Column<bool>(type: "boolean", nullable: true, defaultValue: false),
                    address_id = table.Column<long>(type: "bigint", nullable: true),
                    created_by = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    modified_by = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    created = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    modified = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("contacts_pkey", x => x.id);
                    table.ForeignKey(
                        name: "fk_contacts_address",
                        column: x => x.address_id,
                        principalTable: "address",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "location",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    uuid = table.Column<Guid>(type: "uuid", nullable: false),
                    location_name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    contact_number = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    email_id = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    group_npi_number = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    status = table.Column<bool>(type: "boolean", nullable: true, defaultValue: true),
                    fax = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    address_id = table.Column<long>(type: "bigint", nullable: true),
                    created_by = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    modified_by = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    created = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    modified = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    archive = table.Column<bool>(type: "boolean", nullable: true, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("location_pkey", x => x.id);
                    table.ForeignKey(
                        name: "fk_location_address",
                        column: x => x.address_id,
                        principalTable: "address",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "practice",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    uuid = table.Column<Guid>(type: "uuid", nullable: false),
                    clinic_name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    npi_number = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    tax_type = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    tax_number = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    contact_number = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    email_id = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    taxonomy = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    archive = table.Column<bool>(type: "boolean", nullable: true, defaultValue: false),
                    address_id = table.Column<long>(type: "bigint", nullable: true),
                    created_by = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    modified_by = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    created = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    modified = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("practice_pkey", x => x.id);
                    table.ForeignKey(
                        name: "fk_practice_address",
                        column: x => x.address_id,
                        principalTable: "address",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "role_permission_mapping",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    role_id = table.Column<long>(type: "bigint", nullable: false),
                    permission_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("role_permission_mapping_pkey", x => x.id);
                    table.ForeignKey(
                        name: "fk_role_permission_permission",
                        column: x => x.permission_id,
                        principalTable: "permissions",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_role_permission_role",
                        column: x => x.role_id,
                        principalTable: "roles",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "clinician",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    uuid = table.Column<Guid>(type: "uuid", nullable: false),
                    contact_number = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    npi_number = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    status = table.Column<bool>(type: "boolean", nullable: true, defaultValue: true),
                    archive = table.Column<bool>(type: "boolean", nullable: true, defaultValue: false),
                    languages_spoken = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    supervisor_clinician_uuid = table.Column<Guid>(type: "uuid", nullable: true),
                    user_id = table.Column<long>(type: "bigint", nullable: true),
                    created_by = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    modified_by = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    created = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    modified = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    signature = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    two_factor_authentication = table.Column<bool>(type: "boolean", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("clinician_pkey", x => x.id);
                    table.ForeignKey(
                        name: "fk_clinician_user",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "staff",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    uuid = table.Column<Guid>(type: "uuid", nullable: false),
                    contact_number = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    status = table.Column<bool>(type: "boolean", nullable: true, defaultValue: true),
                    archive = table.Column<bool>(type: "boolean", nullable: true, defaultValue: false),
                    user_id = table.Column<long>(type: "bigint", nullable: true),
                    created_by = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    modified_by = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    created = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    modified = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("staff_pkey", x => x.id);
                    table.ForeignKey(
                        name: "fk_staff_user",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "user_role",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_id = table.Column<long>(type: "bigint", nullable: false),
                    role_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("user_role_pkey", x => x.id);
                    table.ForeignKey(
                        name: "fk_user-role_role",
                        column: x => x.role_id,
                        principalTable: "roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_user-role_user",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "availability",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    uuid = table.Column<Guid>(type: "uuid", nullable: false),
                    time_zone = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    clinician_id = table.Column<long>(type: "bigint", nullable: false),
                    created_by = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    modified_by = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    created = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "now()"),
                    modified = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "now()"),
                    start_date = table.Column<DateOnly>(type: "date", nullable: true),
                    end_date = table.Column<DateOnly>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("availability_pkey", x => x.id);
                    table.ForeignKey(
                        name: "fk_availability_clinician",
                        column: x => x.clinician_id,
                        principalTable: "clinician",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "client",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    uuid = table.Column<Guid>(type: "uuid", nullable: true),
                    preferred_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    date_of_birth = table.Column<DateOnly>(type: "date", nullable: true),
                    legal_sex = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    gender_identity = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    phone_number = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    ethnicity = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    race = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    preferred_language = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    user_id = table.Column<long>(type: "bigint", nullable: true),
                    address_id = table.Column<long>(type: "bigint", nullable: true),
                    emergency_contact_id = table.Column<long>(type: "bigint", nullable: true),
                    guardian_contact_id = table.Column<long>(type: "bigint", nullable: true),
                    primary_clinician_id = table.Column<long>(type: "bigint", nullable: true),
                    referring_clinician_id = table.Column<long>(type: "bigint", nullable: true),
                    phone_appointment_reminder = table.Column<bool>(type: "boolean", nullable: true),
                    email_appointment_remainder = table.Column<bool>(type: "boolean", nullable: true),
                    payment_method = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    active = table.Column<bool>(type: "boolean", nullable: true),
                    archive = table.Column<bool>(type: "boolean", nullable: true),
                    created_by = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    modified_by = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    created = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    modified = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    client_status = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    profile_image_url = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true),
                    two_factor_authentication = table.Column<bool>(type: "boolean", nullable: true),
                    alert_note = table.Column<string>(type: "text", nullable: true),
                    portal_access = table.Column<bool>(type: "boolean", nullable: true),
                    mrn = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("client_pkey", x => x.id);
                    table.ForeignKey(
                        name: "fk_client_address",
                        column: x => x.address_id,
                        principalTable: "address",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_client_emergency_contact",
                        column: x => x.emergency_contact_id,
                        principalTable: "emergency_contact",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_client_guardian_contact",
                        column: x => x.guardian_contact_id,
                        principalTable: "emergency_contact",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_client_primary_clinician",
                        column: x => x.primary_clinician_id,
                        principalTable: "clinician",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_client_referring_clinician",
                        column: x => x.referring_clinician_id,
                        principalTable: "clinician",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_client_user",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "clinician_location_mapping",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    clinician_id = table.Column<long>(type: "bigint", nullable: false),
                    location_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("clinician_location_mapping_pkey", x => x.id);
                    table.ForeignKey(
                        name: "fk_clinician_location_mapping_clinician",
                        column: x => x.clinician_id,
                        principalTable: "clinician",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_clinician_location_mapping_location",
                        column: x => x.location_id,
                        principalTable: "location",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "group_settings",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    clinician_id = table.Column<long>(type: "bigint", nullable: false),
                    group_name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    group_initials = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    cpt_code = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    family_group = table.Column<bool>(type: "boolean", nullable: true, defaultValue: false),
                    archive = table.Column<bool>(type: "boolean", nullable: true, defaultValue: false),
                    created_by = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    modified_by = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    created = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "now()"),
                    modified = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "now()"),
                    uuid = table.Column<Guid>(type: "uuid", nullable: true),
                    bill_to = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("group_settings_pkey", x => x.id);
                    table.ForeignKey(
                        name: "fk_group_settings_clinician",
                        column: x => x.clinician_id,
                        principalTable: "clinician",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "block_days",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    uuid = table.Column<Guid>(type: "uuid", nullable: false),
                    start_time = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    end_time = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    availability_id = table.Column<long>(type: "bigint", nullable: true),
                    created_by = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    modified_by = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    created = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "now()"),
                    modified = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("block_days_pkey", x => x.id);
                    table.ForeignKey(
                        name: "fk_block_days_availability",
                        column: x => x.availability_id,
                        principalTable: "availability",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "day_wise_slots",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    uuid = table.Column<Guid>(type: "uuid", nullable: false),
                    day_of_week = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    start_time = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    end_time = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    availability_id = table.Column<long>(type: "bigint", nullable: true),
                    created_by = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    modified_by = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    created = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "now()"),
                    modified = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("day_wise_slots_pkey", x => x.id);
                    table.ForeignKey(
                        name: "fk_day_wise_slots_availability",
                        column: x => x.availability_id,
                        principalTable: "availability",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "client_insurance",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    uuid = table.Column<Guid>(type: "uuid", nullable: true),
                    insurance_name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    member_id = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    group_id = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    start_date = table.Column<DateOnly>(type: "date", nullable: true),
                    end_date = table.Column<DateOnly>(type: "date", nullable: true),
                    relationship = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    subscriber_first_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    subscriber_last_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    subscriber_birth_date = table.Column<DateOnly>(type: "date", nullable: true),
                    insurance_card_front = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    insurance_card_back = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    client_id = table.Column<long>(type: "bigint", nullable: true),
                    insurance_type = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("client_insurance_pkey", x => x.id);
                    table.ForeignKey(
                        name: "fk_insurance_patient",
                        column: x => x.client_id,
                        principalTable: "client",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "appointment",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    uuid = table.Column<Guid>(type: "uuid", nullable: true),
                    session_type = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    appointment_mode = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    appointment_type = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    status = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    timezone = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    client_id = table.Column<long>(type: "bigint", nullable: true),
                    clinician_id = table.Column<long>(type: "bigint", nullable: true),
                    duration = table.Column<int>(type: "integer", nullable: true),
                    start_time = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    end_time = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    location_id = table.Column<long>(type: "bigint", nullable: true),
                    place_of_service = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    estimated_amount = table.Column<double>(type: "double precision", nullable: true),
                    created_by = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    modified_by = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    created = table.Column<DateTime>(type: "timestamp without time zone", nullable: true, defaultValueSql: "now()"),
                    modified = table.Column<DateTime>(type: "timestamp without time zone", nullable: true, defaultValueSql: "now()"),
                    group_settings_id = table.Column<long>(type: "bigint", nullable: true),
                    base_appointment_id = table.Column<Guid>(type: "uuid", nullable: true),
                    repeat_every = table.Column<int>(type: "integer", nullable: true),
                    recurrence_type = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    number_of_appointments = table.Column<int>(type: "integer", nullable: true),
                    repeat_on_days = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true),
                    month_day = table.Column<int>(type: "integer", nullable: true),
                    note = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("appointment_pkey", x => x.id);
                    table.ForeignKey(
                        name: "fk_appointment_client",
                        column: x => x.client_id,
                        principalTable: "client",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_appointment_clinician",
                        column: x => x.clinician_id,
                        principalTable: "clinician",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_appointment_group_settings",
                        column: x => x.group_settings_id,
                        principalTable: "group_settings",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_appointment_location",
                        column: x => x.location_id,
                        principalTable: "location",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "client_group_settings_mapping",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    group_id = table.Column<long>(type: "bigint", nullable: false),
                    client_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("client_group_settings_mapping_pkey", x => x.id);
                    table.ForeignKey(
                        name: "fk_cgsm_client",
                        column: x => x.client_id,
                        principalTable: "client",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_cgsm_group_settings",
                        column: x => x.group_id,
                        principalTable: "group_settings",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "appointment_cpt_codes",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    uuid = table.Column<Guid>(type: "uuid", nullable: true),
                    cpt_code = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    units = table.Column<int>(type: "integer", nullable: true),
                    appointment_id = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("appointment_cpt_codes_pkey", x => x.id);
                    table.ForeignKey(
                        name: "fk_appointment_appointment_cpt_codes",
                        column: x => x.appointment_id,
                        principalTable: "appointment",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "address_uuid_key",
                table: "address",
                column: "uuid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "uk_address_uuid",
                table: "address",
                column: "uuid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_appointment_client_id",
                table: "appointment",
                column: "client_id");

            migrationBuilder.CreateIndex(
                name: "IX_appointment_clinician_id",
                table: "appointment",
                column: "clinician_id");

            migrationBuilder.CreateIndex(
                name: "IX_appointment_group_settings_id",
                table: "appointment",
                column: "group_settings_id");

            migrationBuilder.CreateIndex(
                name: "IX_appointment_location_id",
                table: "appointment",
                column: "location_id");

            migrationBuilder.CreateIndex(
                name: "IX_appointment_cpt_codes_appointment_id",
                table: "appointment_cpt_codes",
                column: "appointment_id");

            migrationBuilder.CreateIndex(
                name: "IX_availability_clinician_id",
                table: "availability",
                column: "clinician_id");

            migrationBuilder.CreateIndex(
                name: "IX_block_days_availability_id",
                table: "block_days",
                column: "availability_id");

            migrationBuilder.CreateIndex(
                name: "uk_block_days_uuid",
                table: "block_days",
                column: "uuid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_client_address_id",
                table: "client",
                column: "address_id");

            migrationBuilder.CreateIndex(
                name: "IX_client_emergency_contact_id",
                table: "client",
                column: "emergency_contact_id");

            migrationBuilder.CreateIndex(
                name: "IX_client_guardian_contact_id",
                table: "client",
                column: "guardian_contact_id");

            migrationBuilder.CreateIndex(
                name: "IX_client_primary_clinician_id",
                table: "client",
                column: "primary_clinician_id");

            migrationBuilder.CreateIndex(
                name: "IX_client_referring_clinician_id",
                table: "client",
                column: "referring_clinician_id");

            migrationBuilder.CreateIndex(
                name: "IX_client_user_id",
                table: "client",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "uk_client_uuid",
                table: "client",
                column: "uuid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_client_group_settings_mapping_client_id",
                table: "client_group_settings_mapping",
                column: "client_id");

            migrationBuilder.CreateIndex(
                name: "IX_client_group_settings_mapping_group_id",
                table: "client_group_settings_mapping",
                column: "group_id");

            migrationBuilder.CreateIndex(
                name: "IX_client_insurance_client_id",
                table: "client_insurance",
                column: "client_id");

            migrationBuilder.CreateIndex(
                name: "uk_client_insurance_uuid",
                table: "client_insurance",
                column: "uuid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_clinician_user_id",
                table: "clinician",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "uk_clinician_uuid",
                table: "clinician",
                column: "uuid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_clinician_location_mapping_clinician_id",
                table: "clinician_location_mapping",
                column: "clinician_id");

            migrationBuilder.CreateIndex(
                name: "IX_clinician_location_mapping_location_id",
                table: "clinician_location_mapping",
                column: "location_id");

            migrationBuilder.CreateIndex(
                name: "contacts_uuid_key",
                table: "contacts",
                column: "uuid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_contacts_address_id",
                table: "contacts",
                column: "address_id");

            migrationBuilder.CreateIndex(
                name: "uk_contacts_uuid",
                table: "contacts",
                column: "uuid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_day_wise_slots_availability_id",
                table: "day_wise_slots",
                column: "availability_id");

            migrationBuilder.CreateIndex(
                name: "uk_day_wise_slots_uuid",
                table: "day_wise_slots",
                column: "uuid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "uk_emergency_contact_uuid",
                table: "emergency_contact",
                column: "uuid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "forms_uuid_key",
                table: "forms",
                column: "uuid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_group_settings_clinician_id",
                table: "group_settings",
                column: "clinician_id");

            migrationBuilder.CreateIndex(
                name: "IX_location_address_id",
                table: "location",
                column: "address_id");

            migrationBuilder.CreateIndex(
                name: "location_uuid_key",
                table: "location",
                column: "uuid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "uk_location_uuid",
                table: "location",
                column: "uuid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_practice_address_id",
                table: "practice",
                column: "address_id");

            migrationBuilder.CreateIndex(
                name: "practice_uuid_key",
                table: "practice",
                column: "uuid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "uk_practice_uuid",
                table: "practice",
                column: "uuid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_role_permission_mapping_permission_id",
                table: "role_permission_mapping",
                column: "permission_id");

            migrationBuilder.CreateIndex(
                name: "IX_role_permission_mapping_role_id",
                table: "role_permission_mapping",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "IX_staff_user_id",
                table: "staff",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "staff_uuid_key",
                table: "staff",
                column: "uuid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "uk_staff_uuid",
                table: "staff",
                column: "uuid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "sticky_note_uuid_key",
                table: "sticky_note",
                column: "uuid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_user_role_role_id",
                table: "user_role",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_role_user_id",
                table: "user_role",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "uk_user_uuid",
                table: "users",
                column: "uuid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "uk_users_email",
                table: "users",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "uk_users_iam_id",
                table: "users",
                column: "iam_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "users_uuid_key",
                table: "users",
                column: "uuid",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "appointment_cpt_codes");

            migrationBuilder.DropTable(
                name: "block_days");

            migrationBuilder.DropTable(
                name: "client_clinician");

            migrationBuilder.DropTable(
                name: "client_group_settings_mapping");

            migrationBuilder.DropTable(
                name: "client_insurance");

            migrationBuilder.DropTable(
                name: "clinician_location_mapping");

            migrationBuilder.DropTable(
                name: "contacts");

            migrationBuilder.DropTable(
                name: "databasechangelog");

            migrationBuilder.DropTable(
                name: "databasechangeloglock");

            migrationBuilder.DropTable(
                name: "day_wise_slots");

            migrationBuilder.DropTable(
                name: "fee_schedule");

            migrationBuilder.DropTable(
                name: "forms");

            migrationBuilder.DropTable(
                name: "notifications");

            migrationBuilder.DropTable(
                name: "practice");

            migrationBuilder.DropTable(
                name: "role_permission_mapping");

            migrationBuilder.DropTable(
                name: "staff");

            migrationBuilder.DropTable(
                name: "sticky_note");

            migrationBuilder.DropTable(
                name: "user_role");

            migrationBuilder.DropTable(
                name: "appointment");

            migrationBuilder.DropTable(
                name: "availability");

            migrationBuilder.DropTable(
                name: "permissions");

            migrationBuilder.DropTable(
                name: "roles");

            migrationBuilder.DropTable(
                name: "client");

            migrationBuilder.DropTable(
                name: "group_settings");

            migrationBuilder.DropTable(
                name: "location");

            migrationBuilder.DropTable(
                name: "emergency_contact");

            migrationBuilder.DropTable(
                name: "clinician");

            migrationBuilder.DropTable(
                name: "address");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
