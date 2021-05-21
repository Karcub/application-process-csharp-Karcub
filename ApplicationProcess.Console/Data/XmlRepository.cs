using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using Codecool.ApplicationProcess.Entities;

namespace Codecool.ApplicationProcess.Data
{
    /// <summary>
    /// Memory storage for application process.
    /// </summary>
    public class XmlRepository : IApplicationRepository
    {
        private IList<Mentor> _mentors;
        private IList<Applicant> _applicants;
        private IList<Application> _applications;
        private IList<School> _schools;

        /// <summary>
        /// Initializes a new instance of the <see cref="XmlRepository"/> class.
        /// </summary>
        public XmlRepository()
        {
            _mentors = new List<Mentor>();
            _applicants = new List<Applicant>();
            _applications = new List<Application>();
            _schools = new List<School>();

            ReadDataFromXml();
        }

        /// <inheritdoc/>
        public int AmountOfApplicationAfter(DateTime date)
        {
            return _applications.Count(application => application.ApplicationDate > date);
        }

        /// <inheritdoc/>
        public IEnumerable<Mentor> GetAllMentorFrom(City city)
        {
            return from mentor in _mentors
                where mentor.City.Equals(city)
                select mentor;
        }

        /// <inheritdoc/>
        public IEnumerable<Mentor> GetAllMentorWhomFavoriteLanguage(string language)
        {
            return from mentor in _mentors
                where mentor.ProgrammingLanguage.Equals(language)
                select mentor;
        }

        /// <inheritdoc/>
        public IEnumerable<Applicant> GetApplicantsOf(string contactMentorName)
        {
            string[] mentorName = contactMentorName.Split(' ');

            // DOESN'T WORK
            return from application in _applications
                where application.Mentor.FirstName.Equals(mentorName[0]) &&
                      application.Mentor.LastName.Equals(mentorName[1])
                select application.Applicant;
        }

        /// <inheritdoc/>
        public IEnumerable<string> GetAppliedStudentEmailList()
        {
            return from applicant in _applicants
                select applicant.Email;
        }

        private void ReadDataFromXml()
        {
            XmlData cars = null;

            XmlSerializer serializer = new XmlSerializer(typeof(XmlData));

            StreamReader reader = new StreamReader("../../../Resources/Backup.xml", System.Text.Encoding.UTF8);
            cars = (XmlData)serializer.Deserialize(reader);
            foreach (var mentor in cars.Mentors)
            {
                _mentors.Add(mentor);
            }

            foreach (var applicant in cars.Applicants)
            {
                _applicants.Add(applicant);
            }

            foreach (var application in cars.Applications)
            {
                _applications.Add(application);
            }

            foreach (var school in cars.Schools)
            {
                _schools.Add(school);
            }

            reader.Close();
        }
    }
}