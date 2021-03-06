﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Atlassian.Jira;
using JiraTimeBot.Configuration;
using JiraTimeBot.JiraIntegration;
using JiraTimeBot.RepositoryProviders.Interfaces;
using JiraTimeBot.TaskTime.Objects;

namespace JiraTimeBot.RepositoryProviders
{
    public class JiraCommitEmulator : IRepositoryLog
    {
        private readonly ILog _log;
        private readonly JiraApi _jiraApi;

        public JiraCommitEmulator(ILog log, JiraApi jiraApi)
        {
            _log = log;
            _jiraApi = jiraApi;
        }

        public List<TaskTimeItem> GetRepositoryLog(Settings settings,
                                                   string currentRepository,
                                                   DateTime? date = null,
                                                   CancellationToken cancellationToken = default(CancellationToken))
        {
            date = date.GetValueOrDefault(DateTime.Now.Date);
            var workTasks = new List<TaskTimeItem>();
            var issues = _jiraApi.GetIssuesByJQL(settings.JiraQuery, settings, date, cancellationToken);

            foreach (var issue in issues)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    return new List<TaskTimeItem>();
                }
                var comments = issue.GetCommentsAsync(cancellationToken).Result?.ToList() ?? new List<Comment>();

                var user = settings.JiraUserName;
                //user = "Zoya.Aleksandridi";
                var userComments = comments.Where(f => f.Author.Equals(user, StringComparison.InvariantCultureIgnoreCase)).ToList();
                _log.Trace($"Получены комментарии по задаче {issue.Key} ({issue.Type?.Name}): {userComments.Count}.");

                if (userComments.Any())
                {
                    foreach (var comment in userComments.OrderBy(f => f.CreatedDate))
                    {
                        var item = new TaskTimeItem(issue.Key.Value,
                            issue.Summary,
                            issue.Project,
                            issue.Updated.GetValueOrDefault(date.Value),
                            TimeSpan.Zero,
                            1,
                            1,
                            "",
                            CommitType.Task);
                        workTasks.Add(item);
                    }
                }
                else
                {
                    var item = new TaskTimeItem(issue.Key.Value,
                        issue.Summary,
                        issue.Project,
                        issue.Updated.GetValueOrDefault(date.Value),
                        TimeSpan.Zero,
                        1,
                        1,
                        "",
                        CommitType.Task);
                    workTasks.Add(item);
                }
            }

            return workTasks;
        }
    }
}
