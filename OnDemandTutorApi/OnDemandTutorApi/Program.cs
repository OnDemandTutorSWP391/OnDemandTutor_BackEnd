using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using OnDemandTutorApi.BusinessLogicLayer.Helper;
using OnDemandTutorApi.BusinessLogicLayer.Services.IServices;
using OnDemandTutorApi.BusinessLogicLayer.Services.ServicesImpl;
using OnDemandTutorApi.DataAccessLayer.DAO;
using OnDemandTutorApi.DataAccessLayer.Entity;
using OnDemandTutorApi.DataAccessLayer.Repositories.Contracts;
using OnDemandTutorApi.DataAccessLayer.Repositories.RepoImpl;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "OnDemandTutor API", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});

//set up policy
builder.Services.AddCors(opts =>
{
    opts.AddPolicy("corspolicy", build =>
    {
        build.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
    });
});

//set up identity framework
builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<MyDbContext>().AddDefaultTokenProviders();

//set up DB
builder.Services.AddDbContext<MyDbContext>(options => {
    options.UseSqlServer(builder.Configuration.GetConnectionString("OnDemandTutor"));
});

//set up auto mapper
builder.Services.AddAutoMapper(typeof(Program));

//DAO
builder.Services.AddScoped<UserDAO>();
builder.Services.AddScoped<TutorDAO>();
builder.Services.AddScoped<CoinManagementDAO>();
builder.Services.AddScoped<RequestCategoryDAO>();
builder.Services.AddScoped<RequestDAO>();
builder.Services.AddScoped<ResponseDAO>();
builder.Services.AddScoped<LevelDAO>();
builder.Services.AddScoped<SubjectDAO>();
builder.Services.AddScoped<SubjectLevelDAO>();
builder.Services.AddScoped<StudentJoinDAO>();
builder.Services.AddScoped<TimeDAO>();

//Repositories
builder.Services.AddScoped<IUserRepo, UserRepo>();
builder.Services.AddScoped<ITutorRepo, TutorRepo>();
builder.Services.AddScoped<OnDemandTutorApi.DataAccessLayer.Repositories.Contracts.ICoinManagementRepo, CoinManagementRepo>();
builder.Services.AddScoped<IRequestCategoryRepo, RequestCategoryRepo>();
builder.Services.AddScoped<IRequestRepo, RequestRepo>();
builder.Services.AddScoped<IResponseRepo, ResponseRepo>();
builder.Services.AddScoped<ILevelRepo, LevelRepo>();
builder.Services.AddScoped<ISubjectRepo, SubjectRepo>();
builder.Services.AddScoped<ISubjectLevelRepo, SubjectLevelRepo>();
builder.Services.AddScoped<IStudentJoinRepo, StudentJoinRepo>();
builder.Services.AddScoped<ITimeRepo, TimeRepo>();


//Services
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITutorService, TutorService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IAdminService, AdminService>();
builder.Services.AddHttpClient();
builder.Services.AddSingleton<IVnPayService, VnPayService>();
builder.Services.AddScoped<OnDemandTutorApi.BusinessLogicLayer.Services.IServices.ICoinManagementService, CoinManagementService>();
builder.Services.AddScoped<IRequestCategoryService, RequestCategoryService>();
builder.Services.AddScoped<IRequestService, RequestService>();
builder.Services.AddScoped<IResponseService, ResponseService>();
builder.Services.AddScoped<ILevelService, LevelService>();
builder.Services.AddScoped<ISubjectService, SubjectService>();
builder.Services.AddScoped<ISubjectLevelService, SubjectLevelService>();
builder.Services.AddScoped<IStudentJoinService, StudentJoinService>();
builder.Services.AddScoped<ITimeService, TimeService>();


//Add config for Required Email
//builder.Services.Configure<IdentityOptions>(opts => opts.SignIn.RequireConfirmedEmail = true);

//Add config for verify token
builder.Services.Configure<DataProtectionTokenProviderOptions>(opts => opts.TokenLifespan = TimeSpan.FromHours(1));

//Add authentication
builder.Services.AddAuthentication(options => {
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options => {
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = builder.Configuration["JWT:ValidAudience"],
        ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"])),
        ClockSkew = TimeSpan.Zero,
        ValidateLifetime = true,
    };
});

//Add Email Config
var emailConfig = builder.Configuration.GetSection("EmailConfiguration").Get<EmailConfiguration>();
builder.Services.AddSingleton(emailConfig);

//Add VnPay Config
var vnPayConfig = builder.Configuration.GetSection("VnPayConfiguration").Get<VnPayConfiguration>();
builder.Services.AddSingleton(vnPayConfig);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("corspolicy");

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
