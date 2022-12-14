using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities.States
{
	public class StateMachineBase<T> : MonoBehaviour where T : StateMachineBase<T>
	{
		private StateBase<T> m_currentState;
		private StateBase<T> m_nextState;

		public bool ChangeState(StateBase<T> _nextState)
		{
			bool bRet = m_nextState == null;
			m_nextState = _nextState;
			return bRet;
		}

		internal virtual void Update()
		{
			if (m_nextState != null)
			{
				if (m_currentState != null)
				{
					m_currentState.OnExit();
				}
				m_currentState = m_nextState;
				m_currentState.OnEnter();
				m_nextState = null;
			}

			if (m_currentState != null)
			{
				m_currentState.OnUpdate();
			}
		}

        internal virtual void FixedUpdate()
        {
			if (m_currentState != null)
			{
				m_currentState.OnFixedUpdate();
			}
		}

		internal virtual void LateUpdate()
        {
			if (m_currentState != null)
			{
				m_currentState.OnLateUpdate();
			}
		}
	}
}