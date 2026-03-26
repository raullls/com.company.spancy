using com.company.spancy.backend.dto;
using com.company.spancy.backend.service;
using Nancy;
using Nancy.ModelBinding;

namespace com.company.spancy.backend.module
{
    public class StudentModule : NancyModule
    {
        public IStudentService StudentService { get; set; }
        public StudentModule() : base("/api/onetooneselfreference")
        {
            this.Get("/findAll", x =>
            {
                return this.StudentService.FindAllRO();
            });

            this.Get("/findById/{stuid}", x =>
            {
                return this.StudentService.FindByIdRO((long)x.stuid);
            });

            this.Post("/create", x =>
            {
                StudentDto studentDto = this.Bind<StudentDto>();
                Response response = this.StudentService.CreateStudentTX(studentDto) as dynamic;
                return Response.AsJson<Response>(response, response.Errors != null && response.Errors.Count > 0 ? HttpStatusCode.Conflict : HttpStatusCode.OK);
            });

            this.Post("/remove/{stuid}", x =>
            {
                Response response = this.StudentService.RemoveStudentTX((long)x.stuid) as dynamic;
                return Response.AsJson<Response>(response, response.Errors != null && response.Errors.Count > 0 ? HttpStatusCode.Conflict : HttpStatusCode.OK);
            });

            this.Post("/edit", x =>
            {
                StudentDto studentDto = this.Bind<StudentDto>();
                Response response = this.StudentService.UpdateStudentTX(studentDto) as dynamic;
                return Response.AsJson<Response>(response, response.Errors != null && response.Errors.Count > 0 ? HttpStatusCode.Conflict : HttpStatusCode.OK);
            });
        }
    }
}