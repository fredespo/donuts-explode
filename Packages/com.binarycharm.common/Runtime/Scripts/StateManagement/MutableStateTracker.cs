using System.Diagnostics;

namespace BinaryCharm.Common.StateManagement {
    
    /// <summary>
    /// Simple struct to keep track of state updates.
    /// Useful to create polling-based update chains that perform some actions
    /// only when some other element changes.
    /// </summary>
    public struct MutableStateTracker {

        private long m_timestamp;

        /// <summary>
        /// Records a state change.
        /// </summary>
        public void SetChanged() {
            m_timestamp = Stopwatch.GetTimestamp();
        }

        /// <summary>
        /// Records a state change if the <paramref name="dependency"/> state is
        /// newer. Returns a boolean value indicating if the state change 
        /// has been recorded.
        /// </summary>
        /// <param name="dependency">A reference to another MutableStateTracker
        /// struct on which this depends: if the other has been updated, it means
        /// that we need to update this too.</param>
        /// <returns>true if a state change has been recorded, false otherwise.</returns>
        public bool SetChangedIfDependencyChanged(in MutableStateTracker dependency) {
            long iLastChanged = dependency.m_timestamp;
            if (iLastChanged > m_timestamp) {
                m_timestamp = iLastChanged;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Returns a boolean value indicating if the <paramref name="dependency"/> 
        /// state is newer
        /// </summary>
        /// <param name="dependency">A reference to another MutableStateTracker
        /// struct.</param>
        /// <returns>true if <paramref name="dependency"/> state is newer, false otherwise </returns>
        public bool IsDependencyChanged(in MutableStateTracker dependency) {
            long iLastChanged = dependency.m_timestamp;
            return iLastChanged > m_timestamp;
        }

        //// cool but creates garbage!
        //public void SetChangedIfAnyDependencyChanged<T>(
        //        IEnumerable<T> rDependencies,
        //        Func<T, MutableStateTracker> trackerGetter,
        //        Action rUpdateAction = null) {
        //    foreach (var rDep in rDependencies) {
        //        long iLastChanged = trackerGetter(rDep).m_timestamp;
        //        if (iLastChanged > m_timestamp) {
        //            if (rUpdateAction != null) rUpdateAction();
        //            m_timestamp = iLastChanged;
        //            break;
        //        }
        //    }
        //}

    }

}
