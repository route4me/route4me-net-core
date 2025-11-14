const releaseNotesGenOptions = {
    "preset": "conventionalcommits",
    "presetConfig": {
        "issuePrefixes": [
            'APR-',
            'BIL-',
            'PST-',
            'R4MADMIN-',
            'R4MDIRS-',
            'R4MSEO-',
            'R4MTMGWY-',
            'R4MWEB-',
            'RWM-',
            'SCP-',
            'SEC-',
            'TRAN-',
            'WT-'
        ],
        "issueUrlFormat": 'https://route4me.atlassian.net/browse/{{prefix}}{{id}}'
    },
};

const gitOptions = {
    "assets": [
        "package.json",
        "package-lock.json",
        'npm-shrinkwrap.json'
    ],
    "message": "chore(release): ${nextRelease.version} [skip ci]\n\n${nextRelease.notes}"
};

module.exports = {
    "dryRun": false,
    "branches": [
        '+([0-9])?(.{+([0-9]),x}).x',
        'master',
        'next',
        'next-major',
        {name: 'beta', prerelease: true},
        {name: 'alpha', prerelease: true}
    ],
    "plugins": [
        "@semantic-release/commit-analyzer",
        [
            "@semantic-release/release-notes-generator",
            releaseNotesGenOptions
        ],
        "@semantic-release/changelog",
        [
            "@semantic-release/github",
            {
                "successComment": false,
                "failTitle": false
            }
        ],
        [
            "@semantic-release/git",
            gitOptions
        ]
    ]
};