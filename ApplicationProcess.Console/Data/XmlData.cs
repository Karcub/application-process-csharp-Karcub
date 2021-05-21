using System;
using System.Xml.Serialization;
using Codecool.ApplicationProcess.Entities;

namespace Codecool.ApplicationProcess.Data
{
    /// <summary>
    /// String representation of a <see cref="Mentor"/>.
    /// </summary>
    [Serializable]
    [XmlRoot("Data")]
    public class XmlData
    {
        /// <inheritdoc/>
        [XmlArray("Mentors")]
        [XmlArrayItem("Mentor", typeof(Mentor))]
        public Mentor[] Mentors { get; set; }

        /// <inheritdoc/>
        [XmlArray("Schools")]
        [XmlArrayItem("School", typeof(School))]
        public School[] Schools { get; set; }

        /// <inheritdoc/>
        [XmlArray("Applicants")]
        [XmlArrayItem("Applicant", typeof(Applicant))]
        public Applicant[] Applicants { get; set; }

        /// <inheritdoc/>
        [XmlArray("Applications")]
        [XmlArrayItem("Application", typeof(Application))]
        public Application[] Applications { get; set; }
    }
}