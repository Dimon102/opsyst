#include <windows.h>
#include <chrono>
#include <ctime>
#include <iostream>
#include <string>
#include <vector>

using namespace std;
using std::cout;
using std::chrono::duration_cast;
using std::chrono::milliseconds;
using std::chrono::seconds;
using std::chrono::system_clock;

#define MAX_THREADS_NUMBER 10
#define PAUSE 10


float global_start = 1;
float global_end = 5000000;
float h = (float)1;

float integralSum = 0;
int CURR_ITER_NUM = 0;

HANDLE timerMutex;
long START_TIME = -1;
long END_TIME = -1;

HANDLE hThreads[MAX_THREADS_NUMBER];

TCHAR szMessage[5000];
DWORD dwTemp, i;
CONST HANDLE hStdOut = GetStdHandle(STD_OUTPUT_HANDLE);


float func(float x) {
    return sin(cos(x * x)* cos(x * x));
}

float calc_integral(float start, float end) {
    float x = start;
    //float h = (float) 0.01;

    float integral = 0;
    while (x < end) {
        float curr_val = func(x) * h;
        //cout << curr_val << endl;
        integral += curr_val;

        x += h;
    }
    //std::cout << integral << endl;
    return integral;
}

int findThreadNum(HANDLE hThread) {
    for (int i = 0; i < CURR_ITER_NUM; i++)
        if (GetThreadId(hThreads[i]) == GetThreadId(hThread))
            return i;
    return -1;
}


DWORD WINAPI ThreadProc(CONST LPVOID lpParam) {
    WaitForSingleObject(timerMutex, INFINITE);
    ReleaseMutex(timerMutex);
    HANDLE hThread = GetCurrentThread();

    DWORD thread_num = findThreadNum(hThread);
    //cout << "\"" << thread_num << "\"";
    cout << " ";
    float start = global_start + (global_end - global_start) / CURR_ITER_NUM * thread_num;
    float end = start + (global_end - global_start) / CURR_ITER_NUM;
    //std::cout << thread_num << " " << start << " " << end << endl;
    wsprintf(szMessage, TEXT("Thread %d: %s%d%s%d\r\n"), thread_num, " ", (int)start, " ", (int)end);
    WriteConsole(hStdOut, szMessage, lstrlen(szMessage), &dwTemp, NULL);
    std::fill_n(szMessage, lstrlen(szMessage), 0);

    CONST HANDLE hMutex = (CONST HANDLE)lpParam;

    long value = calc_integral(start, end);

    WaitForSingleObject(hMutex, INFINITE);
    integralSum += value;
    END_TIME = duration_cast<milliseconds> (system_clock::now().time_since_epoch()).count();
    ReleaseMutex(hMutex);
    Sleep(PAUSE);

    ExitThread(0);
}


INT main() {
    CURR_ITER_NUM = 1;

    for (; CURR_ITER_NUM <= MAX_THREADS_NUMBER; CURR_ITER_NUM++) {
        timerMutex = CreateMutex(NULL, FALSE, NULL);
        std::cout << "Iter " << CURR_ITER_NUM << ":" << endl;
        integralSum = 0;
        START_TIME = -1;
        END_TIME = -1;

        CONST HANDLE hMutex = CreateMutex(NULL, FALSE, NULL);


        for (int j = 0; j < CURR_ITER_NUM; j++) {
            hThreads[j] = CreateThread(NULL, 0, &ThreadProc, hMutex, 0, NULL);
            if (NULL == hThreads[j]) {
                cout << "Поток не был создан" << endl;
            }
        }

        START_TIME = duration_cast<milliseconds>(system_clock::now().time_since_epoch()).count();
        ReleaseMutex(timerMutex);

        WaitForMultipleObjects(CURR_ITER_NUM, hThreads, TRUE, INFINITE);

        long wtime = END_TIME - START_TIME;

        std::cout << "integral sum = " << integralSum << endl;

        wsprintf(szMessage, TEXT("work time = %dms\r\n\r\n"), (int)wtime);
        WriteConsole(hStdOut, szMessage, lstrlen(szMessage), &dwTemp, NULL);

        for (int j = 0; j < CURR_ITER_NUM; j++) {
            CloseHandle(hThreads[j]);
        }

        //std::fill_n(hThreads, MAX_THREADS_NUMBER, NULL);
        CloseHandle(hMutex);
    }

    ExitProcess(0);
}