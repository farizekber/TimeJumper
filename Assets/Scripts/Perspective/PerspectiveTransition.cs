using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Perspective
{
    class PerspectiveTransition
    {
        public delegate void Trigger();

        public Trigger m_trigger;
        public Perspectives m_from, m_to;

        public PerspectiveTransition(Perspectives from, Perspectives to, Trigger trigger)
        {
            m_from = from;
            m_to = to;
            m_trigger = trigger;
        }
    }
}
