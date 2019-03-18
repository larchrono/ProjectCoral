using System.Collections; 
using System.Collections.Generic; 
using UnityEngine; 
using System;

public class GlobalTimer : MonoBehaviour 
{ 
	private static GlobalTimer instance; 
	public static GlobalTimer Instance 
	{ 
		get 
		{ 
			if (!instance) 
			{ 
				instance = GameObject.FindObjectOfType<GlobalTimer>(); 
			} 
			if (!instance) 
			{ 
				GameObject obj = new GameObject(typeof(GlobalTimer). ToString()); 
				instance = obj.AddComponent<GlobalTimer>(); 
			} 
			return instance; 
		} 
	}

	//正在使用的TimerData 
	private List<TimerData> mUseTimerDatas = new List<TimerData>(); 
	//空閒的TimerData 
	private List<TimerData> mNotUseTimerDatas = new List<TimerData>();

	//嘗試從空閒池中取一個TimerData 
	private TimerData GetTimerData(bool isAdd = true) 
	{ 
		TimerData data = null; 
		if (mNotUseTimerDatas.Count <= 0) 
		{ 
			data = new TimerData(); 
		} 
		else 
		{ 
			data = mNotUseTimerDatas[0 ]; 
			mNotUseTimerDatas.RemoveAt(0); 
		}

		mUseTimerDatas.Add(data);

		return data; 
	}

	//創建一個計時器
	public TimerData AddTimer(float _duration, Action endCallBack, bool _isIgnoreTime = false) 
	{ 
		TimerData data = GetTimerData(); 
		data.Init(_duration, endCallBack, _isIgnoreTime);

		return data; 
	}

	//創建一個重複型計時器
	public TimerData AddIntervalTimer(float _duration, float _interval, Action _endCallBack, Action<float> _intervalCallBack, bool _isIgnoreTime = false) 
	{ 
		TimerData data = GetTimerData(); 
		data.Init(_duration, _endCallBack, _isIgnoreTime, _interval, _intervalCallBack );

		return data; 
	}

	protected void Clear(TimerData data) 
	{ 
		if (mUseTimerDatas.Remove(data)) 
		{ 
			mNotUseTimerDatas.Add(data); 
		} 
		else 
		{ 
			Debug.LogWarning("GlobalTimer not find TimerData"); 
		} 
	}

	#region測試代碼
	void Start() 
	{ 
		TimerData td1 = GlobalTimer.Instance.AddTimer(5, 
			() => 
			{ 
				Debug.Log("5秒後調用該方法1"); 
			});

		TimerData td2 = GlobalTimer.Instance.AddIntervalTimer(5, 1, 
			() => 
			{ 
				Debug.Log("5秒後調用該方法2"); 
			}, 
			(float remainTime) => 
			{ 
				Debug.Log("每一秒調用執行此處：剩餘時間" + remainTime); 
			} 
		);
	} 
	#endregion

	void Update() 
	{ 
		for (int i = 0; i < mUseTimerDatas.Count; ++i) 
		{ 
			if (!mUseTimerDatas[i].Update()) 
			{ 
				//沒更新成功，mUseTimerDatas長度減1，所以需要- -i 
				--i; 
			} 
		} 
	}

	public class TimerData 
	{ 
		//持續時間
		private float mDuration; 
		//重複間隔
		private float mInterval; 
		//結束回調
		private Action mEndCallBack; 
		//每次重複回調
		private Action<float> mIntervalCallBack; 
		//是否忽略時間
		private bool isIgnoreTime; 
		//計時器
		private float mRunTime; 
		//間隔計時器
		private float mRunIntervalTime;

		//初始化
		public void Init(float _duration, Action _endCallBack, bool _isIgnoreTime = false, float _interval = -1f, Action<float> _intervalCallBack = null) 
		{ 
			mDuration = _duration; 
			mInterval = _interval; 
			mEndCallBack = _endCallBack; 
			mIntervalCallBack = _intervalCallBack; 
			isIgnoreTime = _isIgnoreTime; 
			mRunTime = 0; 
			mRunIntervalTime = 0; 
		}

		//更新
		public bool Update() 
		{ 
			float deltaTime = isIgnoreTime ? Time.unscaledDeltaTime : Time.deltaTime; 
			mRunTime += deltaTime; 
			mRunIntervalTime += deltaTime;

			if (mIntervalCallBack != null) 
			{ 
				if (mRunIntervalTime >= mInterval) 
				{ 
					mRunIntervalTime -= mInterval; 
					mIntervalCallBack(mDuration - mRunTime); 
				} 
			}

			if (mRunTime >= mDuration) 
			{ 
				if (mEndCallBack != null) 
				{ 
					mEndCallBack(); 
				} 
				Clear(); 
				return false; 
			}

			return true; 
		}

		public void Clear() 
		{ 
			instance.Clear(this); 
		}

		public void AddEndCallBack(Action _endCallBack) 
		{ 
			mEndCallBack += _endCallBack; 
		} 
	} 
} 